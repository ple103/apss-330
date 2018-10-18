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
    }
}
