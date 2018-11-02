using System;
using System.Collections.Generic;
using System.Text;

namespace mewmont.Models.SocketSenders
{
    class PlaybackStateChange
    {
        public string msg { get; private set; }
        public string token { get; set; }
        public int room { get; set; }
        public int playbackState { get; set; }
        public bool isPrivate {get ;set; }

        public PlaybackStateChange()
        {
            msg = "playbackStateChange";
        }
    }
}
