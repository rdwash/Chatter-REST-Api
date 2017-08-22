using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatterApi.Domain.Entities;

namespace ChatterApi.Domain.Factories
{
    public class MessageFactory
    {
        IChatterApiRepository _repository;
        UserFactory _userFactory;

        public MessageFactory()
        {
            _repository = new ChatterApiRepository(new ChatterApiContext());
            _userFactory = new UserFactory();
        }

        public DTO.Message CreateMessage(Message message)
        {
            var msgUser = _repository.GetUser(message.UserId);

            var messageDTO = new DTO.Message();

            messageDTO.Id = message.Id;
            messageDTO.Type = message.Type;
            messageDTO.Attributes.Message = message.ChatMessage;
            messageDTO.Attributes.Created_At = message.Created_At;
            messageDTO.Relationships.Creator.Data.Id = msgUser.Id;
            messageDTO.Relationships.Creator.Data.Type = msgUser.Type;
            messageDTO.Relationships.Creator.Data.Attributes.Username = msgUser.Username;
            messageDTO.Relationships.Creator.Data.Links.Self = "";
            messageDTO.Relationships.Creator.Links.Self = "";
            messageDTO.Relationships.Creator.Links.Related = "";            
            messageDTO.Included.Add(_userFactory.CreateUser(msgUser));
            messageDTO.Links.Self = "";

            return messageDTO;
        }

        public Message CreateMessage(DTO.Message message)
        {
            return new Message()
            {
                Id = message.Id,
                Type = message.Type,
                ChatMessage = message.Attributes.Message,                
                UserId = message.Relationships.Creator.Data.Id
            };
        }
    }
}