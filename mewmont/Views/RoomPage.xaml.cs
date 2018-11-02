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
using VidyoConnector;

namespace mewmont
{
	public partial class RoomPage : ContentPage
	{
        RoomViewModel thisViewModel;
        HybridWebViewV2 MediaViewerVideo;
        JsonSerializer serializer = new JsonSerializer();
        int RoomId;
        bool IsPrivate;

        IVidyoController _vidyoController = null;
        VidyoViewModel _viewModel = VidyoViewModel.GetInstance();

        public RoomPage(int roomId, bool isPrivate)
		{
			InitializeComponent();
            RoomId = roomId;
            IsPrivate = isPrivate;
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
            MediaViewerVideo.Source = Constants.StreamrAPIUrl + "youtubeview";
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
            //App._vidyoController.SetNativeView(_videoView);
            //App._vidyoController.Connect(Constants.VidyoHost, App.UserManager.User.VidyoToken, App.UserManager.User.Username, App.RoomManager.Room.Id.ToString());
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

        /// <summary>
        /// Prompts user to leave the room, before closing the connection and leaving.
        /// </summary>
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

        /// <summary>
        /// Open the settings page when the settings button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsBtn_OnClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RoomSettingsPage());
        }

        /// <summary>
        /// Update the video when a change to the media has occured.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoomMediaChanged(object sender, System.EventArgs e)
        {
            // Set the correct placeholder image and obtain media data
            Media thisMedia = App.RoomManager.Room.CurrentMedia;
            thisViewModel.PlaceholderImage = thisMedia.thumbnailUrl;

            // Propogate the media change to the media view.
            string JSCommand = "changeMedia('" + thisMedia.mediaId + "', " + thisMedia.playbackState + ", " + thisMedia.currentPosition + ");";
            MediaViewerVideo.InjectJavaScript(JSCommand);
            Debug.WriteLine("=====================================================" + JSCommand);

            // Toggle the play and pause buttons
            if (thisMedia.playbackState == 1)
            {
                thisViewModel.PlayBtnSource = "pause_btn.png";
                thisViewModel.VideoPlaceholderVisible = false;
                pseudoTimeTracker();
            } else
            {
                thisViewModel.PlayBtnSource = "play_btn.png";
            }

            // Updates time value on the view model, to reflect the latest data.
            thisViewModel.TotalDuration = ((thisMedia.totalDuration % 3600) / 60) + ":" + (thisMedia.totalDuration % 60);
            thisViewModel.TotalDurationSeconds = thisMedia.totalDuration;
            thisViewModel.CurrentPositionSeconds = thisMedia.currentPosition;
            thisViewModel.CurrentPosition = ((thisMedia.currentPosition % 3600) / 60) + ":" + (thisMedia.currentPosition % 60);
        }

        private bool isTimeTracking = false;
        private bool isTimeFrozen = false;

        /// <summary>
        /// Update the video time when playing to allow the timebar appearing to be moving.
        /// </summary>
        private void pseudoTimeTracker()
        {
            // Check if the media is playing, and update the time upon every second passed.
            if (!isTimeTracking) { 
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    isTimeTracking = true;

                    if (thisViewModel.CurrentPositionSeconds >= thisViewModel.TotalDurationSeconds)
                    {
                        thisViewModel.CurrentPositionSeconds = 0;
                    }

                    var currentMedia = App.RoomManager.Room.CurrentMedia;
                    // If the media is playing, keep time tracking and update.
                    if (currentMedia.playbackState == 1)
                    {
                        // If time hasn't froze, keep time tracking enabled, and update the current position.
                        if (!isTimeFrozen)
                        {
                            thisViewModel.CurrentPositionSeconds++;
                            thisViewModel.CurrentPosition = (((Convert.ToInt32(thisViewModel.CurrentPositionSeconds)) % 3600) / 60 + ":" + (Convert.ToInt32(thisViewModel.CurrentPositionSeconds)) % 60);
                        }
                        return true;
                    } else
                    {
                        isTimeTracking = false;
                        return false;
                    }
                });
            }
        }

        /// <summary>
        /// Update the current position of the media upon a media time change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaTimeChange(object sender, System.EventArgs e)
        {
            Media thisMedia = App.RoomManager.Room.CurrentMedia;
            int currentPosition = Convert.ToInt32(thisViewModel.CurrentPositionSeconds);

            // Only update the position if it is not equal to the current position to avoid time skipping.
            if (thisMedia.currentPosition != currentPosition)
            {
                thisMedia.currentPosition = currentPosition;
                App.RoomManager.UpdateMedia(thisMedia);
            }
        }

        /// <summary>
        /// Adds the latest message in the room, to the chatlog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageRecieved(object sender, System.EventArgs e)
        {
            ChatBoxContent.ItemsSource = null;
            ChatBoxContent.ItemsSource = App.RoomManager.Room.Chatlog;
            ChatBoxContent.ScrollTo((ChatBoxContent.ItemsSource as List<Message>).Last(), ScrollToPosition.End, true);
        }

        /// <summary>
        /// Updates the room's media upon pressing the play and pause button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Broadcasts a message to the room upon pressing the send button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMessageBtn_Pressed(object sender, System.EventArgs e)
        {
            App.RoomManager.SendMessage(thisViewModel.MessageBody, App.UserManager.User.Username);
            // Clear the send message entry
            thisViewModel.MessageBody = null;
        }

        public void InitializeVidyo()
        {
            // Provide the videoView to the Vidyo controller
            _vidyoController = App._vidyoController;
        }
    }
}
