using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace mewmont.YouTube
{
    public static class YouTubeNavigation
    {
        /// <summary>
        /// Takes a YouTube video ID, and concatenates it with YouTube's iframe player.
        /// </summary>
        /// <param name="videoId">The id of the YouTube video to play. e.g. "j52BZWMOKeY"</param>
        /// <returns></returns>
        public static string AbsoluteYouTubeURL(string videoId)
        {
            return "https://www.youtube.com/embed/" + videoId;
        }

        /// <summary>
        /// Takes a raw URL of a YouTube video, and extracts the video Id.
        /// </summary>
        /// <param name="rawUrl">A URL directing to a YouTube video</param>
        /// <returns></returns>
        public static string YouTubeIDFromRawUrl(string rawUrl)
        {
            Uri rawUri = new Uri(rawUrl);
            string videoId = HttpUtility.ParseQueryString(rawUri.Query).Get("v");
            if (videoId == null)
            {
                if (rawUrl.Contains("https://youtu.be/"))
                {
                    // Attempt another method
                    Uri uri = new Uri(rawUrl);
                    videoId = uri.Segments.Last();
                }
            } if (videoId == null)
            {
                throw new Exception("The video URL is invalid.");
            }
            return videoId;
        }
    }
}
