using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChatterApi.Constants;
using ChatterApi.Domain;
using ChatterApi.Domain.Entities;
using ChatterApi.Domain.Factories;

namespace ChatterApi.API.Controllers
{
    [RoutePrefix("api/v1")]
    public class SessionsController : ApiController
    {
        IChatterApiRepository _repository;
        MessageFactory _messageFactory;

        public SessionsController()
        {
            _repository = new ChatterApiRepository(new ChatterApiContext());
            _messageFactory = new MessageFactory();
        }

        [Route("sessions")]
        public IHttpActionResult Get()
        {
            return Redirect(ChatterApiConstants.IdSrvAuthorize);
        }

        [Route("sessions")]
        public IHttpActionResult Post([FromBody]DTO.Message message)
        {
            
            try
            {
                return Redirect(ChatterApiConstants.IdSrvAuthorize);

                //if (message == null)
                //    return BadRequest();

                //// map
                //var msg = _messageFactory.CreateMessage(message);

                //var result = _repository.InsertMessage(msg);
                //if (result.Status == RepositoryActionStatus.Created)
                //{
                //    // map to dto
                //    var newMsg = _messageFactory.CreateMessage(result.Entity);
                //    return Created(Request.RequestUri + "/" + newMsg.Id.ToString(), newMsg);
                //}

                //return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
