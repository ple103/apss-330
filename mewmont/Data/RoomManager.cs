using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using mewmont.Models;
using mewmont.YouTube;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;
using Xamarin.Forms;

namespace mewmont.Data
{
    public class RoomManager
    {
        IRestService restService;
        WebSocketService webSocketService;
        YoutubeClient youtubeClient;
        public Room Room { private set; get; }
        public List<Room> Rooms { private set; get; }

        public event EventHandler MediaChanged;
        public event EventHandler MessageRecieved;

        public RoomManager(WebSocketService wsService)
        {
            restService = App.restService;
            webSocketService = wsService;
            youtubeClient = new YoutubeClient();
            wsService.DataRecieved += new EventHandler<RoomSocket>(WebSocketDataRecieved);
        }

        public async Task CreateRoom(string title, string rawMediaUrl)
        {
            Room sendingRoom = new Room();
            sendingRoom.Title = title;
            sendingRoom.CurrentMedia = new Media(YouTubeNavigation.YouTubeIDFromRawUrl(rawMediaUrl));

            Room = await restService.PutRoomData(sendingRoom);
        }

        public async Task StartRoomConnection(int id)
        {
            await GetRoomData(id);
            webSocketService.StartLoadingData(id);
        }

        public void CloseRoomConnection()
        {
            webSocketService.StopLoadingData();
        }

        public async Task<bool> GetRoomData(int id)
        {
            Room = await restService.GetRoomData(id);
            return true;
        }

        public async Task<List<Room>> GetRoomsData()
        {
            Rooms = await restService.GetRoomsData();
            return Rooms;
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
                case "message":
                    Room.AddMessage(response.messageData);
                    MessageRecieved?.Invoke(this, new EventArgs());
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

        public void UpdateMedia(Media media)
        {
            webSocketService.UpdateMedia(media);
        }

        public void SetRoomPlaybackState(int playBackState)
        {
            webSocketService.PlaybackStateChange(playBackState);
        }

        public void GetRoomMedia()
        {
            webSocketService.GetMedia();
        }
        public void SendMessage(string message, string author)
        {
            webSocketService.SendMessage(message, author);
        }
    }
}
