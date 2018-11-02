using System;
using System.Collections.Generic;
using System.Drawing;

namespace mewmont.Models
{
    public class Room
    {
        private int id;
        private string name;
        private Media currentMedia;
        private Permission defaultPermissions;
        private List<Message> chatlog = new List<Message>();
        private RoomMember[] roomMembers;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

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
            get;
            set;
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

        public void AddMessage(Message message)
        {
            chatlog.Add(message);
        }

        public void AssignColourToMember(RoomMember member, Color colour)
        {
            throw new NotImplementedException();
        }
    }
}
