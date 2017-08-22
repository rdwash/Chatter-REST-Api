using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using ChatterApi.Domain;
using ChatterApi.Domain.Entities;
using ChatterApi.Domain.Factories;
using Marvin.JsonPatch;

namespace ChatterApi.API.Controllers
{
    [RoutePrefix("api/v1")]
    public class MessagesController : ApiController
    {
        IChatterApiRepository _repository;
        MessageFactory _messageFactory;

        const int maxPageSize = 10;

        public MessagesController()
        {
            _repository = new ChatterApiRepository(new ChatterApiContext());
            _messageFactory = new MessageFactory();
        }


        [Route("messages", Name = "MessagesList")]
        public IHttpActionResult Get(int page = 1, int pageSize = maxPageSize)
        {
            try
            {
                var msgs = _repository.GetMessages();

                if (msgs == null)
                {
                    return NotFound();
                }

                // ensure the page size isn't larger than the maximum.
                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                // calculate data for metadata
                var totalCount = msgs.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var urlHelper = new UrlHelper(Request);

                var prevLink = page > 1 ? urlHelper.Link("MessageList",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                    }) : "";
                var nextLink = page < totalPages ? urlHelper.Link("MessageList",
                    new
                    {
                        page = page + 1,
                        pageSize = pageSize,
                    }) : "";


                var paginationHeader = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalCount = totalCount,
                    totalPages = totalPages,
                    previousPageLink = prevLink,
                    nextPageLink = nextLink
                };

                HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));


                var msgsResult = msgs
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToList()
                    .Select(msg => _messageFactory.CreateMessage(msg));

                return Ok(msgsResult);

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }



        [Route("messages/{userId}/user/{id}")]
        [Route("messages/{id}")]
        public IHttpActionResult Get(int id, int? userId = null)
        {
            try
            {
                Message message = null;

                if (userId == null)
                {
                    message = _repository.GetMessage(id);
                }
                else
                {
                    var messagesForUser = _repository.GetMessages((int)userId);

                    // if the user doesn't exist, we shouldn't try to get the messages
                    if (messagesForUser != null)
                    {
                        message = messagesForUser.FirstOrDefault(eg => eg.Id == id);
                    }
                }

                if (message != null)
                {
                    var returnValue = _messageFactory.CreateMessage(message);
                    return Ok(returnValue);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("messages/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeleteMessage(id);

                if (result.Status == RepositoryActionStatus.Deleted)
                    return StatusCode(HttpStatusCode.NoContent);

                if (result.Status == RepositoryActionStatus.NotFound)
                    return NotFound();

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("messages")]
        public IHttpActionResult Post([FromBody]DTO.Message message)
        {
            try
            {
                if (message == null)
                    return BadRequest();

                // map
                var msg = _messageFactory.CreateMessage(message);

                var result = _repository.InsertMessage(msg);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto
                    var newMsg = _messageFactory.CreateMessage(result.Entity);
                    return Created(Request.RequestUri + "/" + newMsg.Id.ToString(), newMsg);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("messages/{id}")]
        public IHttpActionResult Put(int id, [FromBody]DTO.Message message)
        {
            try
            {
                if (message == null)
                    return BadRequest();

                // map
                var msg = _messageFactory.CreateMessage(message);

                var result = _repository.UpdateMessage(msg);
                if (result.Status == RepositoryActionStatus.Updated)
                {
                    // map to dto
                    var updatedMessage = _messageFactory.CreateMessage(result.Entity);
                    return Ok(updatedMessage);
                }
                else if (result.Status == RepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("messages/{id}")]
        public IHttpActionResult Patch(int id, [FromBody]JsonPatchDocument<DTO.Message> messagePatchDocument)
        {
            try
            {
                // find 
                if (messagePatchDocument == null)
                    return BadRequest();

                var message = _repository.GetMessage(id);
                if (message == null)
                    return NotFound();

                //// map
                var msg = _messageFactory.CreateMessage(message);

                // apply changes to the DTO
                messagePatchDocument.ApplyTo(msg);

                // map the DTO with applied changes to the entity, & update
                var result = _repository.UpdateMessage(_messageFactory.CreateMessage(msg));

                if (result.Status == RepositoryActionStatus.Updated)
                {
                    // map to dto
                    var updatedMessage = _messageFactory.CreateMessage(result.Entity);
                    return Ok(updatedMessage);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

    }
}
