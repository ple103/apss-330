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
