using System;

namespace mewmont.Models
{
    public class CameraStream
    {
        private Media videoStream;
        private Media audioStream;
        private bool audioMuted;
        private RoomMember roomOwner;
        private bool isSpeaking;

        public Media VideoStream
        {
            get { return videoStream; }
            set { videoStream = value; }
        }

        public Media AudioStream
        {
            get { return audioStream; }
            set { audioStream = value; }
        }

        public bool AudioMuted
        {
            get { return audioMuted; }
            set { audioMuted = value; }
        }

        public RoomMember RoomOwner
        {
            get { return roomOwner; }
            set { roomOwner = value; }
        }

        public bool IsSpeaking
        {
            get { return isSpeaking; }
            set { isSpeaking = value; }
        }

        public void MuteAudio()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
