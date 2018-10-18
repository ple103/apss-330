﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using mewmont.Models;
using System.Net.WebSockets;
using System.Threading.Tasks;
using mewmont.Models.SocketSenders;

namespace mewmont.Data
{
    public class RestService : IRestService
    {
        HttpClient client;
        ClientWebSocket webSocketClient;

        public Room Room { get; private set; }
        public User user { get; private set; }
        public List<Room> Rooms { get; private set; }

        public RestService()
        {
            #region forbasicauthentication
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            #endregion

            webSocketClient = new ClientWebSocket();
        }

        public async Task<Room> PutRoomData(Room creatingRoom)
        {
            var uri = new Uri(string.Format(Constants.StreamrAPIUrl + "room", string.Empty));
            string creatingRoomJson = JsonConvert.SerializeObject(creatingRoom);

            var sendingContent = new StringContent(creatingRoomJson, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PutAsync(uri, sendingContent);
                if (response.IsSuccessStatusCode)
                {
                    var recievingContent = await response.Content.ReadAsStringAsync();
                    Room = JsonConvert.DeserializeObject<Room>(recievingContent);
                    Debug.WriteLine(@"              SUCCESS fetching items");

                }
                else
                {
                    Debug.WriteLine(@"               ERROR while fetching items: {0}", response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"              ERROR Exception Caught while fetching items: {0}", ex.Message);
            }

            return Room;
        }

        public async Task<Room> GetRoomData(int id)
        {
            Room = new Room();
            #region use_RESTAPI_to_get_data
            var uri = new Uri(string.Format(Constants.StreamrAPIUrl + "room/" + id, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Room = JsonConvert.DeserializeObject<Room>(content);
                    Debug.WriteLine(@"              SUCCESS fetching items");

                }
                else
                {
                    Debug.WriteLine(@"               ERROR while fetching items: {0}", response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"              ERROR Exception Caught while fetching items: {0}", ex.Message);
            }
            #endregion
            return Room;
        }

        public async Task<List<Room>> GetRoomsData()
        {
            Rooms = new List<Room>();
            #region use_RESTAPI_to_get_data
            var uri = new Uri(string.Format(Constants.StreamrAPIUrl + "rooms/", string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Rooms = JsonConvert.DeserializeObject<List<Room>>(content);
                    Debug.WriteLine(@"              SUCCESS fetching items");

                }
                else
                {
                    Debug.WriteLine(@"               ERROR while fetching items: {0}", response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"              ERROR Exception Caught while fetching items: {0}", ex.Message);
            }
            #endregion
            return Rooms;
        }

        public async Task<User> Login(Login loginData)
        {
            user = new User();
            #region use_RESTAPI_to_get_data
            var uri = new Uri(string.Format(Constants.StreamrAPIUrl + "account", string.Empty));

            string loginDataJson = JsonConvert.SerializeObject(loginData);

            var sendingContent = new StringContent(loginDataJson, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(uri, sendingContent);
                if (response.IsSuccessStatusCode)
                {
                    var recievingContent = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(recievingContent);
                    Debug.WriteLine(@"              SUCCESS fetching items");

                }
                else
                {
                    Debug.WriteLine(@"               ERROR while fetching items: {0}", response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"              ERROR Exception Caught while fetching items: {0}", ex.Message);
            }
            #endregion
            return user;
        }
    }
}
