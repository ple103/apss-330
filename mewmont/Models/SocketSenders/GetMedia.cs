using System;
using System.Collections.Generic;
using System.Text;

namespace mewmont.Models.SocketSenders
{
    class GetMedia
    {
        public string msg { get; private set; }
        public string token { get; set; }
        public int room { get; set; }

        public GetMedia()
        {
            msg = "getMedia";
        }
    }
}
