using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using mewmont.Models;
using mewmont.Models.SocketSenders;

namespace mewmont.Data
{
    public class UserManager
    {
        IRestService restService;
        public User User;

        public UserManager()
        {
            restService = App.restService;
        }

        public async Task Login(string username, string password)
        {
            Login sendingLoginData = new Login();
            sendingLoginData.username = username;
            sendingLoginData.password = password;

            User = await restService.Login(sendingLoginData);
        }

        public async Task<SuccessResponse> Logout()
        {
            Logout sendingLogoutData = new Logout();
            sendingLogoutData.id = User.Id;
            sendingLogoutData.token = User.Token;

            return await restService.Logout(sendingLogoutData);
        }

        public async Task<SuccessResponse> Register(string username, string password)
        {
            Login sendingRegistrationData = new Login();
            sendingRegistrationData.username = username;
            sendingRegistrationData.password = password;

            return await restService.Register(sendingRegistrationData);
        }
    }
}
