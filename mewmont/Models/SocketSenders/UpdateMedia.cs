using System;
using System.Collections.Generic;
using System.Text;

namespace mewmont.Models.SocketSenders
{
    public class UpdateMedia
    {
        public string msg { get; private set; }
        public string token { get; set; }
        public int room { get; set; }
        public Media media { get; set; }
        public bool isPrivate { get; set; }

        public UpdateMedia()
        {
            msg = "updateMedia";
        }
    }
}
