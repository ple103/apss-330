using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using mewmont.Models;
using mewmont.YouTube;

namespace mewmont
{
	public partial class RoomPage : ContentPage
	{
		public RoomPage()
		{
			InitializeComponent();
		}

        Room room;

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            room = await App.RoomManager.GetTasksAsync();
            MediaViewer.Source = YouTubeNavigation.AbsoluteYouTubeURL(room.media);
        }

        private void MessageEntry_Focused(object sender, FocusEventArgs e)
        {
            OptionButtons.IsVisible = false;
        }

        private void MessageEntry_Unfocused(object sender, FocusEventArgs e)
        {
            OptionButtons.IsVisible = true;
        }

        async void ChatModeBtn_OnClick(object sender, EventArgs e)
        {
            var option = await DisplayActionSheet("Choose a chat mode", "Cancel", null, "Text-only", "Voice", "Camera & Voice");
        }

        async void LeaveRoom_Triggered(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Are you sure?", "Do you want to leave this room?", "Yes", "No");
        }
    }
}
