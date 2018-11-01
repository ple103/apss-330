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
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            webSocketClient = new ClientWebSocket();
        }

        /// <summary>
        /// Puts the specified room onto the list of rooms, either public or private, on the server.
        /// </summary>
        /// <param name="creatingRoom">The room that is to be created on the server.</param>
        /// <returns>The room that was created</returns>
        public async Task<Room> PutRoomData(Room creatingRoom)
        {
            var uri = new Uri(string.Format(Constants.StreamrAPIUrl + "room", string.Empty));
            string creatingRoomJson = JsonConvert.SerializeObject(creatingRoom);

            var sendingContent = new StringContent(creatingRoomJson, Encoding.UTF8, "application/json");
            // Attempt to send the information to the server
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

        /// <summary>
        /// Retrieves a room from the server
        /// </summary>
        /// <param name="id">The ID of the room to retrieve</param>
        /// <returns>The room retrieved from the server</returns>
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

        /// <summary>
        /// Retrieves a list of public rooms from the server
        /// </summary>
        /// <returns>A list of rooms from the server</returns>
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

        /// <summary>
        /// Provides credidentials to the server to log in as a user
        /// </summary>
        /// <param name="loginData">The login credidentials of the user to log into</param>
        /// <returns>The model of the user that was logged into, and returned from the server</returns>
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

        /// <summary>
        /// Sends a signal to the server to disconnect the user from the current session
        /// </summary>
        /// <param name="logoutData">The data for logging out.</param>
        /// <returns>Whether the log out was successful, or not</returns>
        public async Task<SuccessResponse> Logout(Logout logoutData)
        {
            SuccessResponse SuccessResponse = null;
            #region use_RESTAPI_to_get_data
            var uri = new Uri(string.Format(Constants.StreamrAPIUrl + "account/logout", string.Empty));

            string logoutDataJson = JsonConvert.SerializeObject(logoutData);

            var sendingContent = new StringContent(logoutDataJson, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(uri, sendingContent);
                if (response.IsSuccessStatusCode)
                {
                    var recievingContent = await response.Content.ReadAsStringAsync();
                    SuccessResponse = JsonConvert.DeserializeObject<SuccessResponse>(recievingContent);
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
            return SuccessResponse;
        }

        /// <summary>
        /// Sends registration data to the server to create a user.
        /// </summary>
        /// <param name="registerData">The properties of the user account to be created.</param>
        /// <returns>The user that was registered, and returned from the server.</returns>
        public async Task<SuccessResponse> Register(Login registerData)
        {
            var uri = new Uri(string.Format(Constants.StreamrAPIUrl + "account", string.Empty));
            string registerJson = JsonConvert.SerializeObject(registerData);
            SuccessResponse RegistrationResponse = null;
            var sendingContent = new StringContent(registerJson, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PutAsync(uri, sendingContent);
                if (response.IsSuccessStatusCode)
                {
                    var recievingContent = await response.Content.ReadAsStringAsync();
                    RegistrationResponse = JsonConvert.DeserializeObject<SuccessResponse>(recievingContent);
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

            return RegistrationResponse;
        }
    }
}
