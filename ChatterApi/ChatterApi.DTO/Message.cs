using System;
using System.Collections.Generic;

namespace ChatterApi.DTO
{
    public class Message
    {
        public int Id { get; set; }        
        public string Type { get; set; }
        public MessageAttributes Attributes { get; set; }
        public MessageLinks Links { get; set; }
        public Relationships Relationships { get; set; }
        public List<DTO.User> Included { get; set; }

        public Message()
        {
            Attributes = new MessageAttributes();
            Links = new MessageLinks();
            Relationships = new Relationships();
            Included = new List<DTO.User>();
        }      
    }

    public class MessageAttributes
    {
        public MessageAttributes()
        {
                
        }
        public DateTime Created_At { get; set; }
        public string Message { get; set; }
    }
    public class MessageLinks
    {
        public MessageLinks()
        {

        }
        public string Self { get; set; }
    }
}