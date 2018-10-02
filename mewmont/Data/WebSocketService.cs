﻿using System;
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
            ChangeMedia data = new ChangeMedia();
            data.videoId = videoId;
            data.room = roomId;
            data.token = token;

            string sendingData = JsonConvert.SerializeObject(data);
            await SendData(sendingData);
        }


        public async void StartLoadingData()
        {
            ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri("ws://streamr.australiaeast.cloudapp.azure.com:1337/"), CancellationToken.None);

            var roomConnectData = "{\"room\":\"1\", \"msg\":\"connect\"}";
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
            try
            {
                if (ws.State != WebSocketState.Closed)
                    await ws.CloseAsync(WebSocketCloseStatus.Empty, String.Empty, CancellationToken.None);
            }
            finally
            {
                ws.Dispose();
            }
        }

        /// <summary>
        /// Parses plain txt update to CryptoCurrencyUpdate
        /// sample input: 42["m","0~Bitfinex~BTC~USD~1~159021061~1515488499~0.119~15091.30724464~1795.86556211216~1f"]
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Return null if parsing was not possible</returns>
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