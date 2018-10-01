using System;
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

namespace mewmont.Data
{
    public class RestService : IRestService
    {
        HttpClient client;
        ClientWebSocket webSocketClient;

        public Room Room { get; private set; }

        public RestService()
        {
            #region forbasicauthentication
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            #endregion

            webSocketClient = new ClientWebSocket();
        }

        public async Task<Room> GetRoomData()
        {
            Room = new Room();
            #region use_RESTAPI_to_get_data
            var uri = new Uri(string.Format("http://streamr.australiaeast.cloudapp.azure.com/room/1", string.Empty));

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
    }
}
