using System;
using System.Collections.Generic;
using System.Text;

namespace mewmont.Models.SocketSenders
{
    public class SendMessage
    {
        public Message messageData { get; set; }
        public string token { get; set; }
        public string msg { get; set; }
        public int room { get; set; }
        public bool isPrivate { get; set; }

        public SendMessage()
        {
            msg = "message";
        }
    }
}
