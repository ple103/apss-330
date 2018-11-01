using System;
using System.Collections.Generic;
using mewmont.Models.SocketSenders;
using Xamarin.Forms;

namespace mewmont
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        void CreateStreamBtn_Pressed(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new mewmont.CreateStreamPage());
        }

        void JoinStreamBtn_Pressed(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new mewmont.JoinStreamPage());
        }

        void LogOut_Triggered(object sender, EventArgs e)
        {
            Logout();
        }

        protected override bool OnBackButtonPressed()
        {
            Logout();
            return false;
        }

        /// <summary>
        /// Prompt the user if they want to log out, and then proceed to the logout page.
        /// </summary>
        async void Logout()
        {
            var answer = await DisplayAlert("Are you sure?", "Do you want to log out of Streamr?", "Yes", "No");
            // If the user has selected to leave the room
            if (answer)
            {
                SuccessResponse response = await App.UserManager.Logout();
                await Navigation.PushAsync(new LoginPage());
                Navigation.RemovePage(this);
            }
        }
    }
}
