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
            App.RoomManager.MessageRecieved += new EventHandler(MessageRecieved);
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

        private async void MediaViewerLoaded(object sender, EventArgs e)
        {
            // Only initiate the room connection once the media viewer content has loaded,
            // as any calls to the media viewer video beforehand will be discarded.
            await App.RoomManager.StartRoomConnection(RoomId);
            thisViewModel.IsLoading = false;
            ChatBoxContent.ItemsSource = App.RoomManager.Room.Chatlog;
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

        void LeaveRoom_Triggered(object sender, EventArgs e)
        {
            LeaveRoom();
        }

        protected override bool OnBackButtonPressed()
        {
            LeaveRoom();
            return false;
        }

        async void LeaveRoom()
        {
            var answer = await DisplayAlert("Are you sure?", "Do you want to leave this room?", "Yes", "No");
            // If the user has selected to leave the room
            if (answer)
            {
                App.RoomManager.CloseRoomConnection();
                await Navigation.PopAsync();
            }
        }

            private void SettingsBtn_OnClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RoomSettingsPage());
        }

        private void RoomMediaChanged(object sender, System.EventArgs e)
        {
            Media thisMedia = App.RoomManager.Room.CurrentMedia;
            thisViewModel.PlaceholderImage = thisMedia.thumbnailUrl;
            string JSCommand = "changeMedia('" + thisMedia.mediaId + "', " + thisMedia.playbackState + ", " + thisMedia.currentPosition + ");";
            MediaViewerVideo.InjectJavaScript(JSCommand);
            Debug.WriteLine("=====================================================" + JSCommand);
            if (thisMedia.playbackState == 1)
            {
                thisViewModel.VideoPlaceholderVisible = false;
            }
            thisViewModel.TotalDuration = ((thisMedia.totalDuration % 3600) / 60) + ":" + (thisMedia.totalDuration % 60);
            thisViewModel.TotalDurationSeconds = thisMedia.totalDuration;
            thisViewModel.CurrentPositionSeconds = thisMedia.currentPosition;
        }

        private void MediaTimeChange(object sender, System.EventArgs e)
        {
            Media thisMedia = App.RoomManager.Room.CurrentMedia;
            int currentPosition = Convert.ToInt32(thisViewModel.CurrentPositionSeconds);
            if (thisMedia.currentPosition != currentPosition)
            {
                thisMedia.currentPosition = currentPosition;
                App.RoomManager.UpdateMedia(thisMedia);
            }
        }

        private void MessageRecieved(object sender, System.EventArgs e)
        {
            ChatBoxContent.ItemsSource = null;
            ChatBoxContent.ItemsSource = App.RoomManager.Room.Chatlog;
            ChatBoxContent.ScrollTo((ChatBoxContent.ItemsSource as List<Message>).Last(), ScrollToPosition.End, true);
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

        private void SendMessageBtn_Pressed(object sender, System.EventArgs e)
        {
            App.RoomManager.SendMessage(thisViewModel.MessageBody, App.UserManager.User.Username);
            // Clear the send message entry
            thisViewModel.MessageBody = null;
        }
    }
}
