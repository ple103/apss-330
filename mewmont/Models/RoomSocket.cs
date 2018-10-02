using System;
using System.Collections.Generic;
using System.Text;

namespace mewmont.Models
{
    public class RoomSocket
    {
        public string message { get; set; }
        public string token { get; set; }
        public string mediaId { get; set; }
        public int room { get; set; }
    }
}
