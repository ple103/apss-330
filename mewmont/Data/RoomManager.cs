using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using mewmont.Models;
using mewmont.YouTube;

namespace mewmont.Data
{
    public class RoomManager
    {
        IRestService restService;
        WebSocketService webSocketService;


        public event EventHandler<string> TokenRecieved;
        public event EventHandler<string> MediaChanged;

        public RoomManager(IRestService service, WebSocketService wsService)
        {
            restService = service;
            webSocketService = wsService;
            wsService.StartLoadingData();
            wsService.DataRecieved += new EventHandler<RoomSocket>(WebSocketDataRecieved);
        }

        public Task<Room> GetRoomData()
        {
            return restService.GetRoomData();
        }

        private void WebSocketDataRecieved(object sender, dynamic response)
        {
            if (response.message == "allocation")
            {
                TokenRecieved?.Invoke(this, response.token);
                webSocketService.token = response.token;
                webSocketService.roomId = response.room;
            } else if (response.message == "mediaChange")
            {
                MediaChanged?.Invoke(this, response.mediaId);
            }
        }

        public void SetRoomMedia(string rawUrl)
        {
            string videoId = YouTubeNavigation.YouTubeIDFromRawUrl(rawUrl);
            webSocketService.ChangeMedia(videoId);
        }
    }
}
