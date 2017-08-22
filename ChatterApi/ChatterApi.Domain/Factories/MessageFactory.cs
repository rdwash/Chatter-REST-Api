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
        public MessageFactory()
        {

        }

        public DTO.Message CreateMessage(Message message)
        {
            return new DTO.Message()
            {
                Id = message.Id,
                ChatMessage = message.ChatMessage,
                Type = message.Type,
                UserId = message.UserId
            };
        }

        public Message CreateMessage(DTO.Message message)
        {
            return new Message()
            {
                Id = message.Id,
                ChatMessage = message.ChatMessage,
                Type = message.Type,
                UserId = message.UserId
            };
        }
    }
}
