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
    /// <summary>
    /// Holds the current user, and serves as an intermediate gateway for
    /// room based functionality on the server.
    /// </summary>
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
            Room = new Room();
            restService = App.restService;
            webSocketService = wsService;
            youtubeClient = new YoutubeClient();
            wsService.DataRecieved += new EventHandler<RoomSocket>(WebSocketDataRecieved);
        }

        /// <summary>
        /// Create a room using the information provided by the user.
        /// </summary>
        /// <param name="title">The title of the room</param>
        /// <param name="rawMediaUrl">The URL of the YouTube video</param>
        /// <returns></returns>
        public async Task CreateRoom(string title, string rawMediaUrl, string passkey)
        {
            Room sendingRoom = new Room();
            sendingRoom.Title = title;
            sendingRoom.Passkey = passkey;

            // Extracts the video ID from the YouTube URL that was provided by the user
            sendingRoom.CurrentMedia = new Media(YouTubeNavigation.YouTubeIDFromRawUrl(rawMediaUrl));

            // Set the RoomManager's room to that was created on the server
            Room = await restService.PutRoomData(sendingRoom);
        }

        /// <summary>
        /// Starts the WebSocket connection with the server
        /// </summary>
        /// <param name="id">The ID of the room to initiate a websocket connection with</param>
        /// <returns></returns>
        public async Task StartRoomConnection(int id)
        {
            if (Room.Passkey == null)
            {
                await GetRoomData(id);
            } else
            {
                await GetPrivateRoomData(Room.Passkey);
            }
            webSocketService.StartLoadingData(id);
        }

        /// <summary>
        /// Closes the room connection with the server
        /// </summary>
        public void CloseRoomConnection()
        {
            webSocketService.StopLoadingData();
        }

        /// <summary>
        /// Sets the current Room to that specified from the server
        /// </summary>
        /// <param name="id">The ID of the room to retrieve</param>
        /// <returns></returns>
        public async Task<bool> GetRoomData(int id)
        {
            Room = await restService.GetRoomData(id);
            return true;
        }

        /// <summary>
        /// Sets the current Room to that specified from the server
        /// </summary>
        /// <param name="passkey">The passkey of the private room to retrieve</param>
        /// <returns></returns>
        public async Task<bool> GetPrivateRoomData(string passkey)
        {
            webSocketService.isPrivate = true;
            Room = await restService.GetPrivateRoomData(passkey);
            return true;
        }

        /// <summary>
        /// Gets a list of all the public rooms from the server
        /// </summary>
        /// <returns>All of the public rooms that are open on the server.</returns>
        public async Task<List<Room>> GetRoomsData()
        {
            Rooms = await restService.GetRoomsData();
            return Rooms;
        }

        /// <summary>
        /// Handles incoming websocket events, acts accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="response"></param>
        private void WebSocketDataRecieved(object sender, RoomSocket response)
        {
            switch (response.message)
            {
                // The user has been added to the room
                case "allocation":
                    webSocketService.token = response.token;
                    webSocketService.roomId = response.room;
                    GetRoomMedia();
                    break;

                // There is a change in the state of the media
                case "mediaChange":
                    Room.CurrentMedia = response.media;
                    MediaChanged?.Invoke(this, new EventArgs());
                    break;

                // A message from a user, that was broadcasted to the room is recieved
                case "message":
                    Room.AddMessage(response.messageData);
                    MessageRecieved?.Invoke(this, new EventArgs());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sets the current media of the room to that provided from a YouTube site URL.
        /// </summary>
        /// <param name="rawUrl"></param>
        public void SetRoomMedia(string rawUrl)
        {
            string videoId = YouTubeNavigation.YouTubeIDFromRawUrl(rawUrl);
            webSocketService.ChangeMedia(videoId);
        }

        /// <summary>
        /// Updates the room's media with the state of the local media.
        /// </summary>
        /// <param name="media"></param>
        public void UpdateMedia(Media media)
        {
            webSocketService.UpdateMedia(media);
        }

        /// <summary>
        /// Sets the playback state of media for the current room.
        /// </summary>
        /// <param name="playBackState"></param>
        public void SetRoomPlaybackState(int playBackState)
        {
            webSocketService.PlaybackStateChange(playBackState);
        }

        /// <summary>
        /// Retrieves the current media from the room.
        /// </summary>
        public void GetRoomMedia()
        {
            webSocketService.GetMedia();
        }

        /// <summary>
        /// Sends a message to the room that will be broadcasted to all users in the room.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="author"></param>
        public void SendMessage(string message, string author)
        {
            webSocketService.SendMessage(message, author);
        }
    }
}
