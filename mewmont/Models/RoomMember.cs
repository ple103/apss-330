using System;
using System.Drawing;

namespace mewmont.Models
{
    public class RoomMember
    {
        private Permission[] permissions;
        private Color colour;
        private CommMode commMode;

        public Permission[] Permissions
        {
            get { return permissions; }
            set { permissions = value; }
        }

        public Color Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        public CommMode CommMode
        {
            get { return commMode; }
            set { commMode = value; }
        }

        // Implement when message data type available
        //public SendMessage()

        public void LeaveRoom()
        {
            throw new NotImplementedException();
        }
    }

    public enum CommMode { TEXT_ONLY, VIDEO };
}
