using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Timers;
using mewmont.Models;
using Xamarin.Forms;

namespace mewmont
{
    public class RoomViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // The height of the media, to properly display a 16x9 aspect ratio media player.
        public double MediaHeight
        {
            get
            {
                return generateMediaHeight();
            }
        }

        public double MediaWidth
        {
            get
            {
                return App.ScreenWidth;
            }
        }

        // Whether the bottom left row of buttons are visible.
        private bool optionsBtnsVisible = true;
        public bool OptionsBtnsVisible
        {
            set
            {
                if (optionsBtnsVisible != value)
                {
                    optionsBtnsVisible = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("OptionsBtnsVisible"));
                    }
                }
            }
            get
            {
                return optionsBtnsVisible;
            }
        }

        // The placeholder image overlapping the media player when the video is not running.
        private string placeholderImage = "streamr_loading.jpg";
        public string PlaceholderImage
        {
            set
            {
                if (placeholderImage != value)
                {
                    placeholderImage = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("PlaceholderImage"));
                    }
                }
            }
            get
            {
                return placeholderImage;
            }
        }

        private bool isLoading = true;
        public bool IsLoading
        {
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsLoading"));
                        PropertyChanged(this, new PropertyChangedEventArgs("IsLoaded"));
                    }
                }
            }
            get
            {
                return isLoading;
            }
        }

        private bool videoPlaceholderVisible = true;
        public bool VideoPlaceholderVisible
        {
            set
            {
                if (videoPlaceholderVisible != value)
                {
                    videoPlaceholderVisible = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("VideoPlaceholderVisible"));
                    }
                }
            }
            get
            {
                return videoPlaceholderVisible;
            }
        }

        public bool IsLoaded
        {
            get
            {
                return !isLoading;
            }
        }

        /// <summary>
        /// Uses the width of the screen to calculate a suitable height for a
        /// 16x9 aspect ratio box.
        /// </summary>
        /// <returns></returns>
        private double generateMediaHeight()
        {
            double mediaAspectRatio = 16.0 / 9.0;

            double PageWidth = App.ScreenWidth;
            return PageWidth / mediaAspectRatio;
        }

        /// <summary>
        /// List of messages previously sent in the room
        /// </summary>
        public List<Message> Messages
        {
            get
            {
                return App.RoomManager.Room.Chatlog;
            }
        }

        /// <summary>
        /// The timer for the duration between messages.
        /// </summary>
        public Timer messageTimer = new Timer
            {

                // Limit the rate of messages to 3000 milliseconds.
                Interval = 3000,
                // Ensure the message timer doesn't run repeatedly.
                AutoReset = false
            };

        public void startMessageTimer()
        {
            messageTimer.Start();
        }

    /// <summary>
    /// The message typed by the user to broadcast to the room
    /// </summary>
    private string messageBody;
        public string MessageBody
        {
            set
            {
                if (messageBody != value)
                {
                    messageBody = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("MessageBody"));
                    }
                }
            }
            get
            {
                return messageBody;
            }
        }

        /// <summary>
        /// The total duration of the media formatte as HH:MM
        /// </summary>
        private string totalDuration;
        public string TotalDuration
        {
            set
            {
                if (totalDuration != value)
                {
                    totalDuration = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TotalDuration"));
                    }
                }
            }
            get
            {
                return totalDuration;
            }
        }

        /// <summary>
        /// The total duration of the media
        /// </summary>
        private double totalDurationSeconds = 1;
        public double TotalDurationSeconds
        {
            set
            {
                if (totalDurationSeconds != value)
                {
                    totalDurationSeconds = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TotalDurationSeconds"));
                    }
                }
            }
            get
            {
                return totalDurationSeconds;
            }
        }

        /// <summary>
        /// The current position of the media formatted as HH:MM
        /// </summary>
        private string currentPosition;
        public string CurrentPosition
        {
            set
            {
                if (currentPosition != value)
                {
                    currentPosition = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CurrentPosition"));
                    }
                }
            }
            get
            {
                return currentPosition;
            }
        }

        /// <summary>
        /// The current position
        /// </summary>
        private double currentPositionSeconds = 0;
        public double CurrentPositionSeconds
        {
            set
            {
                if (currentPositionSeconds != value)
                {
                    currentPositionSeconds = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CurrentPositionSeconds"));
                    }
                }
            }
            get
            {
                return currentPositionSeconds;
            }
        }

        private string playBtnSource = "play_btn.png";
        public string PlayBtnSource
        {
            set
            {
                if (playBtnSource != value)
                {
                    playBtnSource = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("PlayBtnSource"));
                    }
                }
            }
            get
            {
                return playBtnSource;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
