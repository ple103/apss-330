namespace mewmont.Models
{
    public class Permission
    {
        private bool videoMode = false;
        private bool mediaChange = false;
        private bool mediaPlaybackState = false;
        private int chatFrequency;

        public bool VideoMode
        {
            get { return videoMode; }
            set { videoMode = value; }
        }

        public bool MediaChange
        {
            get { return mediaChange; }
            set { mediaChange = value; }
        }

        public bool MediaPlaybackState
        {
            get { return mediaPlaybackState; }
            set { mediaPlaybackState = value; }
        }

        public int ChatFrequency
        {
            get { return chatFrequency; }
            set { chatFrequency = value; }
        }
    }
}
