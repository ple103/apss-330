using Xamarin.Forms;

namespace mewmont.Models
{
    public class User
    {
        private string username;
        private string emailAddress;
        private Image profilePicture;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public Image ProfilePicture
        {
            get { return profilePicture; }
            set { profilePicture = value; }
        }
    }
}
