using System;

namespace ChatterApi.DTO
{
    public class Message
    {
        public int Id { get; set; }

        public string ChatMessage { get; set; }

        public string Type { get; set; }

        public DateTime Created_At { get; set; }

        public int UserId { get; set; }
    }
}