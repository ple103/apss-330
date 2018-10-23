using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using mewmont.Models;
using Newtonsoft.Json;
using mewmont.Models.SocketSenders;

namespace mewmont.Data
{
    public class WebSocketService : IDisposable
    {
        public event EventHandler<RoomSocket> DataRecieved;
        ClientWebSocket ws;
        JsonSerializer serializer = new JsonSerializer();
        public string token { get; set; }
        public int roomId { get; set; }

        public WebSocketService()
        {
            
        }

        private async Task SendData(String str)
        {
            var encoded2 = Encoding.UTF8.GetBytes(str);
            var buffer2 = new ArraySegment<Byte>(encoded2, 0, encoded2.Length);
            await ws.SendAsync(buffer2, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async void ChangeMedia(string videoId)
        {
            MediaChange data = new MediaChange();
            data.videoId = videoId;
            data.room = roomId;
            data.token = token;

            string sendingData = JsonConvert.SerializeObject(data);
            await SendData(sendingData);
        }

        public async void UpdateMedia(Media media)
        {
            UpdateMedia data = new UpdateMedia();
            data.media = media;
            data.room = roomId;
            data.token = token;

            string sendingData = JsonConvert.SerializeObject(data);
            await SendData(sendingData);
        }

        public async void SendMessage(string message, string author)
        {
            Message messageData = new Message();
            messageData.Author = author;
            messageData.MessageBody = message;
            messageData.SendTime = DateTime.Now;
            SendMessage data = new SendMessage();
            data.messageData = messageData;
            data.token = token;
            data.room = roomId;

            string sendingData = JsonConvert.SerializeObject(data);
            await SendData(sendingData);
        }

        public async void PlaybackStateChange(int state)
        {
            PlaybackStateChange data = new PlaybackStateChange();
            data.playbackState = state;
            data.room = roomId;
            data.token = token;

            string sendingData = JsonConvert.SerializeObject(data);
            await SendData(sendingData);
        }

        public async void GetMedia()
        {
            GetMedia data = new GetMedia();
            data.room = roomId;
            data.token = token;

            string sendingData = JsonConvert.SerializeObject(data);
            await SendData(sendingData);
        }


        public async void StartLoadingData(int roomId)
        {
            ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri(Constants.StreamrWSUrl), CancellationToken.None);

            var roomConnectData = "{\"room\":\"" + roomId + "\", \"msg\":\"connect\"}";
            await SendData(roomConnectData);

            ArraySegment<Byte> readbuffer = new ArraySegment<byte>(new Byte[8192]);

            // While the websocket is open, continue to get updates from server.
            while (ws.State == WebSocketState.Open)
            {
                RoomSocket updateValue = null;
                try
                {
                    var result = await ws.ReceiveAsync(readbuffer, CancellationToken.None);
                    var str = System.Text.Encoding.Default.GetString(readbuffer.Array, readbuffer.Offset, result.Count);
                    updateValue = Parse(str);
                    if (updateValue != null)
                    {
                        DataRecieved?.Invoke(this, updateValue);
                    }
                }
                catch (TaskCanceledException)
                {
                    System.Diagnostics.Debug.Write("WebSocket Stopped");
                }
            }
        }

        public async void StopLoadingData()
        {
            var disconnectData = "{\"token\":\"" + token + "\", \"msg\":\"disconnect\"}";
            try
            {
                if (ws.State != WebSocketState.Closed)
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, disconnectData, CancellationToken.None);
            }
            finally
            {
                ws.Dispose();
            }
        }

        private RoomSocket Parse(String str)
        {
            return JsonConvert.DeserializeObject<RoomSocket>(str);
        }

        public void Dispose()
        {
            if (ws != null)
            {
                ws.Dispose();
                ws = null;
            }
        }
    }
}
