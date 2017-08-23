using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatterApi.DTO
{
    public class IncomingMessage
    {
        public IncomingMessage()
        {
            Data = new SimpleMessage();
        }
        public SimpleMessage Data { get; set; }
    }
}
