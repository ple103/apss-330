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
using XLabs.Serialization.JsonNET;

namespace mewmont
{
	public partial class RoomPage : ContentPage
	{
        RoomViewModel thisViewModel;
        HybridWebViewV2 MediaViewerVideo;
        JsonSerializer serializer = new JsonSerializer();
        int RoomId;

        public RoomPage(int roomId)
		{
			InitializeComponent();
            RoomId = roomId;
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
            MediaViewerVideo.Source = "https://streamrqut.be/youtubeview";
            MediaViewerVideo.SetBinding(HybridWebViewV2.HeightRequestProperty, "MediaHeight");
            MediaViewerVideo.SetBinding(HybridWebViewV2.IsVisibleProperty, "IsLoaded");
            MediaViewerVideo.BindingContext = thisViewModel;
            MediaViewerVideo.InputTransparent = true;
            MediaViewer.Children.Add(MediaViewerVideo);
            MediaViewer.LowerChild(MediaViewerVideo);
            MediaViewerVideo.LoadFinished += new EventHandler(MediaViewerLoaded);
            MediaViewerVideo.RegisterCallback("videoPlaying", (arg) =>
                {
                    mewmont.Models.Javascript.PlaybackState playbackState = serializer.Deserialize<mewmont.Models.Javascript.PlaybackState>(arg);
                    if (playbackState.playbackState != 1)
                    {
                        MediaViewerPlaceholder.IsVisible = true;
                    } else
                    {
                        MediaViewerPlaceholder.IsVisible = false;
                    }
                });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void MediaViewerLoaded(object sender, EventArgs e)
        {
            // Only initiate the room connection once the media viewer content has loaded,
            // as any calls to the media viewer video beforehand will be discarded.
            App.RoomManager.StartRoomConnection(RoomId);
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
            Media thisMedia = App.RoomManager.Room.CurrentMedia;
            thisViewModel.PlaceholderImage = thisMedia.thumbnailUrl;
            MediaViewerVideo.InjectJavaScript("changeMedia('" + thisMedia.mediaId + "', " + thisMedia.playbackState +    ");");
            if (thisMedia.playbackState == 1)
            {
                thisViewModel.VideoPlaceholderVisible = false;
            }
        }

        private void PlayPause_OnClick(object sender, System.EventArgs e)
        {
            if (App.RoomManager.Room.CurrentMedia.playbackState == 0)
            {
                App.RoomManager.SetRoomPlaybackState(1);
            } else if (App.RoomManager.Room.CurrentMedia.playbackState == 1)
            {
                App.RoomManager.SetRoomPlaybackState(0);
            }
        }
    }
}
