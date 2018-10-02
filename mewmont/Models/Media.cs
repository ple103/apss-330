using System;

namespace mewmont.Models
{
    public class Media
    {
        public Media(string mediaId)
        {
            this.mediaId = mediaId;
        }

        // We will use DateTime for now. I will create a Time class to abstract this later.
        private DateTime currentPosition;
        // This may change to a video object (once we create it) instead of it being a Uri.
        public string mediaId { get; set; }
        private DateTime totalDuration;
        private string title;
        private PlaybackState playbackState;

        public bool Play()
        {
            throw new NotImplementedException();
        }

        public bool Pause()
        {
            throw new NotImplementedException();
        }

        public bool Stop()
        {
            throw new NotImplementedException();
        }
    }

    enum PlaybackState { PLAYING, PAUSED, STOPPED };
}
