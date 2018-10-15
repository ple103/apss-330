using System;
using System.Collections.Generic;

using Xamarin.Forms;
using mewmont.Data;

namespace mewmont
{
    public partial class CreateStreamPage : ContentPage
    {
        public CreateStreamPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create and connect to a room upon pressing 'Start'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void StartBtn_Pressed(object sender, EventArgs e)
        {
            try
            {
                await App.RoomManager.CreateRoom(RoomTitleEntry.Text, MediaIdEntry.Text);
                await Navigation.PushAsync(new RoomPage(App.RoomManager.Room.Id));
            } catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
