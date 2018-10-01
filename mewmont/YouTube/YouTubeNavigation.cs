using System;
using System.Collections.Generic;
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

        public static string YouTubeIDFromRawUrl(string rawUrl)
        {
            Uri rawUri = new Uri(rawUrl);
            string videoId = HttpUtility.ParseQueryString(rawUri.Query).Get("v");
            return videoId;
        }
    }
}
