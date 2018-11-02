using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace mewmont
{
    public partial class JoinPrivateStreamPage : ContentPage
    {
        public JoinPrivateStreamPage()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Attempt to join a private room with the same passkey that was entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void EnterBtn_Pressed(object sender, EventArgs e)
        {
            bool success = await App.RoomManager.GetPrivateRoomData(PasskeyEntry.Text);
            // If a room with the passkey exists, join the room.
            if (success)
            {
                App.RoomManager.Room.Passkey = PasskeyEntry.Text;
                var roomPage = new RoomPage(App.RoomManager.Room.Id, true);
                await Navigation.PushAsync(roomPage);
            } else
            {
                await DisplayAlert("Error", "Incorrect passkey. Please try again.", "Ok");
            }
        }
    }
}
