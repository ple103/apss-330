using System;
using System.Collections.Generic;
using System.Text;

namespace mewmont.Models
{
    public class RoomDefinitions
    {
        IDictionary<int, string> PlaybackStates = new Dictionary<int, string>()
                                            {
                                                {0,"Paused"},
                                                {1, "Playing"}
                                            };
    }

    public class Room
    {
        public string title { get; set; }
        public string owner { get; set; }
        public int id { get; set; }
        public string media { get; set; }
        public int playbackState { get; set; }
    }
}
