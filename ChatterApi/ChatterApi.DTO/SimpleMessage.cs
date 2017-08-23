using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatterApi.DTO
{
    public class SimpleMessage
    {
        public string Type { get; set; }
        public MessageAttributes Attributes { get; set; }

        public SimpleMessage()
        {
            Attributes = new MessageAttributes();
        }
    }    
}
