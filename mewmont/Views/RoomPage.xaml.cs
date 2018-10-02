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

        public RoomPage()
		{
			InitializeComponent();
            thisViewModel = ((RoomViewModel)this.BindingContext);
            App.RoomManager.MediaChanged += new EventHandler(RoomMediaChanged);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);

            await App.RoomManager.StartRoomConnection();
            thisViewModel.IsLoading = false;
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

        private void RoomMediaChanged(object sender, System.EventArgs e)
        {
            thisViewModel.MediaURL = YouTubeNavigation.AbsoluteYouTubeURL(App.RoomManager.Room.CurrentMedia.mediaId);
        }
    }
}
