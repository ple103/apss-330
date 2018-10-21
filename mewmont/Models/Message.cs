using System;

namespace mewmont.Models
{
    public class Message
    {
        private string messageBody;
        private DateTime sendTime;
        private string author;

        public string MessageBody
        {
            get { return messageBody; }
            set { messageBody = value; }
        }

        public DateTime SendTime
        {
            get { return sendTime; }
            set { sendTime = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }
    }
}
