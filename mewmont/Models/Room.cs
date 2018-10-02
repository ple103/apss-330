using System;
using System.Collections.Generic;
using System.Drawing;

namespace mewmont.Models
{
    public class Room
    {
        private string name;
        private Media currentMedia;
        private Permission defaultPermissions;
        private string passkey;
        private List<Message> chatlog;
        private RoomMember[] roomMembers;

        public string Title
        {
            get { return name; }
            set { name = value; }
        }

        public Media CurrentMedia
        {
            get { return currentMedia; }
            set { currentMedia = value; }
        }

        public Permission DefaultPermissions
        {
            get { return defaultPermissions; }
            set { defaultPermissions = value; }
        }

        public string Passkey
        {
            get { return passkey; }
            set { passkey = value; }
        }

        public List<Message> Chatlog
        {
            get { return chatlog; }
            set { chatlog = value; }
        }

        public RoomMember[] RoomMembers
        {
            get { return roomMembers; }
            set { roomMembers = value; }
        }

        public bool DestroyRoom()
        {
            throw new NotImplementedException();
        }

        public void ChangeMedia(Media newMedia)
        {
            throw new NotImplementedException();
        }

        public void ChangePermissions()
        {
            throw new NotImplementedException();
        }

        public void PromoteGuestToMember()
        {
            throw new NotImplementedException();
        }

        public void AssignColourToMember(RoomMember member, Color colour)
        {
            throw new NotImplementedException();
        }
    }
}
