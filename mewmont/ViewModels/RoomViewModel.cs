using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace mewmont
{
    public class RoomViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        private double generateMediaHeight()
        {
            double mediaAspectRatio = 16.0 / 9.0;

            double PageWidth = App.ScreenWidth;
            return PageWidth / mediaAspectRatio;
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
