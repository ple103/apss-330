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

        private string mediaURL;
        public string MediaURL
        {
            set
            {
                if (mediaURL != value)
                {
                    mediaURL = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("MediaURL"));
                    }
                }
            }
            get
            {
                return mediaURL;
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
