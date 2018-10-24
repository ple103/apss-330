using System;
using Xamarin.Forms;

namespace mewmont.Models
{
    public class User
    {
        private string username;
        private string token;
        private int id;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Token
        {
            get { return token; }
            set { token = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string VidyoToken { get; set; }

        public async void Logout()
        {
            throw new NotImplementedException();
        }

        // Implement when Room data type created
        //public async void JoinRoom() { }

        public void ChangeProfileInformation()
        {
            throw new NotFiniteNumberException();
        }
    }
}
