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
        RoomViewModel thisViewModel;
        Room room;
        private bool firstApperance = true;

        public RoomPage()
		{
			InitializeComponent();
            thisViewModel = ((RoomViewModel)this.BindingContext);
            App.RoomManager.TokenRecieved += new EventHandler<string>(RoomTokenReceived);
            App.RoomManager.MediaChanged += new EventHandler<string>(RoomMediaChanged);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);

            room = await App.RoomManager.GetRoomData();
            thisViewModel.MediaURL = YouTubeNavigation.AbsoluteYouTubeURL(room.media);
            firstApperance = false;
        }

        private void MessageEntry_Focused(object sender, FocusEventArgs e)
        {
            thisViewModel.OptionsBtnsVisible = false;
        }

        private void MessageEntry_Unfocused(object sender, FocusEventArgs e)
        {
            thisViewModel.OptionsBtnsVisible = true;
        }

        async void ChatModeBtn_OnClick(object sender, EventArgs e)
        {
            var option = await DisplayActionSheet("Choose a chat mode", "Cancel", null, "Text-only", "Voice", "Camera & Voice");
        }

        async void LeaveRoom_Triggered(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Are you sure?", "Do you want to leave this room?", "Yes", "No");
        }

        private void SettingsBtn_OnClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RoomSettingsPage());
        }

        private void RoomTokenReceived(object sender, string token)
        {
            thisViewModel.IsLoading = false;
        }

        private void RoomMediaChanged(object sender, string mediaId)
        {
            thisViewModel.MediaURL = YouTubeNavigation.AbsoluteYouTubeURL(mediaId);
        }
    }
}
