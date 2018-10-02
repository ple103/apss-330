using System;
using System.Collections.Generic;
using System.Text;

namespace mewmont.Models.SocketSenders
{
    class ChangeMedia
    {
        public string msg { get; private set; }
        public string token { get; set; }
        public int room { get; set; }
        public string videoId { get; set; }

        public ChangeMedia()
        {
            msg = "changeMedia";
        }
    }
}
