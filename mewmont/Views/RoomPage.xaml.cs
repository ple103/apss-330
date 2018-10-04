using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using mewmont.Models;
using mewmont.YouTube;
using XLabs.Forms.Controls;

namespace mewmont
{
	public partial class RoomPage : ContentPage
	{
        RoomViewModel thisViewModel;
        HybridWebViewV2 MediaViewerVideo;

        public RoomPage()
		{
			InitializeComponent();
            thisViewModel = ((RoomViewModel)this.BindingContext);
            App.RoomManager.MediaChanged += new EventHandler(RoomMediaChanged);

            createMediaViewerVideo();
        }

        /// <summary>
        /// Due to issues with the XLabs library, the HybridWebView control will only load if called programatically in code.
        /// </summary>
        private void createMediaViewerVideo()
        {
            MediaViewerVideo = new HybridWebViewV2() { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            MediaViewerVideo.Source = "https://streamr.australiaeast.cloudapp.azure.com/youtubeview";
            MediaViewerVideo.SetBinding(HybridWebViewV2.HeightRequestProperty, "MediaHeight");
            MediaViewerVideo.SetBinding(HybridWebViewV2.IsVisibleProperty, "IsLoaded");
            MediaViewerVideo.BindingContext = thisViewModel;
            MediaViewer.Children.Add(MediaViewerVideo);
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
            MediaViewerVideo.InjectJavaScript("changeMedia('" + App.RoomManager.Room.CurrentMedia.mediaId + "');");
        }
    }
}
