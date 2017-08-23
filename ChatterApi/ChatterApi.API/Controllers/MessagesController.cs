using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using ChatterApi.Domain;
using ChatterApi.Domain.Entities;
using ChatterApi.Domain.Factories;
using Marvin.JsonPatch;
using Thinktecture.IdentityModel.WebApi;

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

        [ResourceAuthorize("Read", "Message")]
        [Route("messages", Name = "MessagesList")]
        public IHttpActionResult Get(int page = 1, int pageSize = maxPageSize)
        {
            try
            {
                //Claim iss = null, sub = null;
                //string userId;
                //var identity = this.User.Identity as ClaimsIdentity;

                //if(identity != null)
                //{
                //    iss = identity.FindFirst("iss");
                //    sub = identity.FindFirst("sub");
                //}

                //if(iss != null && sub != null)
                //{
                //    userId = iss.Value + "_" + sub.Value;
                //}
                //else
                //{
                //    return StatusCode(HttpStatusCode.Forbidden);
                //}

                var msgs = _repository.GetMessages();

                if (msgs == null || !msgs.Any())
                    return NotFound();

                // ensure the page size isn't larger than the maximum.
                if (pageSize > maxPageSize)
                    pageSize = maxPageSize;

                CreatePaginationLinks(page, pageSize, msgs);

                var msgsResult = msgs
                    .OrderBy(i => i.Id)
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToList()
                    .Select(msg => _messageFactory.CreateMessage(msg));

                return Ok(msgsResult);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private void CreatePaginationLinks(int page, int pageSize, IQueryable<Message> msgs)
        {
            // calculate data for metadata
            var totalCount = msgs.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);

            var firstLink = page != 1 ? urlHelper.Link("MessagesList",
                new
                {
                    page = 1,
                    pageSize = pageSize,
                }) : null;

            var prevLink = page > 1 ? urlHelper.Link("MessagesList",
                new
                {
                    page = page - 1,
                    pageSize = pageSize,
                }) : null;
            var nextLink = page < totalPages ? urlHelper.Link("MessagesList",
                new
                {
                    page = page + 1,
                    pageSize = pageSize,
                }) : null;

            var lastLink = page != totalPages ? urlHelper.Link("MessagesList",
                new
                {
                    page = page - 1,
                    pageSize = pageSize,
                }) : null;

            var paginationHeader = new
            {
                currentPage = page,
                pageSize = pageSize,
                totalCount = totalCount,
                totalPages = totalPages,
                self = urlHelper.Link("MessagesList",
                new
                {
                    page = 1,
                    pageSize = pageSize,
                }),
                first = firstLink,
                prev = prevLink,
                next = nextLink,
                last = lastLink
            };

            HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));
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
        
        [Route("messages")]
        public IHttpActionResult Post([FromBody]DTO.IncomingMessage data)
        {
            try
            {
                if (data == null)
                    return BadRequest();

                // map
                var msg = _messageFactory.CreateMessage(data);

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
    }
}
