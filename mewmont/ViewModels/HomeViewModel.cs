using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using mewmont.Models;
using Xamarin.Forms;

namespace mewmont
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Username
        {
            get
            {
                if (App.UserManager.User == null)
                {
                    return "Guest";
                }
                else
                {
                    return App.UserManager.User.Username;
                }
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
