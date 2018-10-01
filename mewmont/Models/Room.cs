using System;
using System.Collections.Generic;
using System.Text;

namespace mewmont.Models
{
    public class Room
    {
        public string title { get; set; }
        public string owner { get; set; }
        public int id { get; set; }
        public string media { get; set; }
        public int playbackState { get; set; }
    }
}
