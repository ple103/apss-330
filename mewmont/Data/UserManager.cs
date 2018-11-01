using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using mewmont.Models;
using mewmont.Models.SocketSenders;

namespace mewmont.Data
{
    /// <summary>
    /// Holds the current user, and serves as an intermediate gateway for
    /// user account services on the server.
    /// </summary>
    public class UserManager
    {
        IRestService restService;
        public User User;

        public UserManager()
        {
            // Sets the rest API to use, from the main app class.
            restService = App.restService;
        }

        /// <summary>
        /// Sends a request to the REST API to login using the provided crededentials.
        /// The returned user is then set as the current user for this UserManager.
        /// </summary>
        /// <param name="username">The username of the user to log into</param>
        /// <param name="password">The password of the user to log into</param>
        /// <returns>The user model of the user that was logged into</returns>
        public async Task Login(string username, string password)
        {
            Login sendingLoginData = new Login();
            sendingLoginData.username = username;
            sendingLoginData.password = password;

            User = await restService.Login(sendingLoginData);
        }

        /// <summary>
        /// Attempts to end the current user session, essentially logging out
        /// from the server, and destroying the current user token.
        /// </summary>
        /// <returns>A conditional value of whether the user was successfully logged out, or not.</returns>
        public async Task<SuccessResponse> Logout()
        {
            Logout sendingLogoutData = new Logout();
            sendingLogoutData.id = User.Id;
            sendingLogoutData.token = User.Token;

            return await restService.Logout(sendingLogoutData);
        }

        /// <summary>
        /// Attempts to register the user with the crededentials provided.
        /// </summary>
        /// <param name="username">The username of the user to register</param>
        /// <param name="password">The password of the user to register</param>
        /// <returns>A conditional vaue of whether the user account was successfully created, or not.</returns>
        public async Task<SuccessResponse> Register(string username, string password)
        {
            Login sendingRegistrationData = new Login();
            sendingRegistrationData.username = username;
            sendingRegistrationData.password = password;

            return await restService.Register(sendingRegistrationData);
        }
    }
}
