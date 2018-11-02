using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mewmont.Models.SocketSenders;

namespace mewmont
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// Register the user, and return the login page if it was successful.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RegisterBtn_Pressed(object sender, EventArgs e)
        {
            // Register the user
            SuccessResponse response = await App.UserManager.Register(UsernameEntry.Text, PasswordEntry.Text);

            // Clear the password text for security
            PasswordEntry.Text = "";

            if (response == null || !response.success)
            {
                await DisplayAlert("Error", "Please enter different details and try again.", "OK");
            }
            else
            {
                await DisplayAlert("Account Created", "You're account has successfully been created, please go back and login", "OK");
                await Navigation.PopAsync();
            }
        }

        /// <summary>
        /// Check if the entry lengths are of acceptable size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void EntryTextChanged(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            if (entry.Text.Length > 16)
            {
                entry.Text = entry.Text.Substring(0, 16);
                await DisplayAlert("Warning", "The username or password cannot be longer than 16 characters.", "Oh ok");
            }
        }
    }
}
