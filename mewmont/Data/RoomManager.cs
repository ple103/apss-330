using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using mewmont.Models;
using mewmont.YouTube;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace mewmont.Data
{
    public class RoomManager
    {
        IRestService restService;
        WebSocketService webSocketService;
        YoutubeClient youtubeClient;
        public Room Room { private set; get; }

        public event EventHandler MediaChanged;

        public RoomManager(IRestService service, WebSocketService wsService)
        {
            restService = service;
            webSocketService = wsService;
            youtubeClient = new YoutubeClient();
            wsService.DataRecieved += new EventHandler<RoomSocket>(WebSocketDataRecieved);
        }

        public async Task<bool> StartRoomConnection()
        {
            await GetRoomData();
            webSocketService.StartLoadingData();
            return true;
        }

        public async Task<bool> GetRoomData()
        {
            Room = await restService.GetRoomData();
            return true;
        }

        private void WebSocketDataRecieved(object sender, RoomSocket response)
        {
            switch (response.message)
            {
                case "allocation":
                    webSocketService.token = response.token;
                    webSocketService.roomId = response.room;
                    GetRoomMedia();
                    break;

                case "mediaChange":
                    Room.CurrentMedia = response.media;
                    MediaChanged?.Invoke(this, new EventArgs());
                    break;
                default:
                    break;
            }
        }

        public void SetRoomMedia(string rawUrl)
        {
            string videoId = YouTubeNavigation.YouTubeIDFromRawUrl(rawUrl);
            webSocketService.ChangeMedia(videoId);
        }

        public void GetRoomMedia()
        {
            webSocketService.GetMedia();
        }
    }
}
