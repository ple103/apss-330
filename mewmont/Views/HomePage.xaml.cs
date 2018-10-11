using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace mewmont
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new mewmont.Views.CreateStreamPage());
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new mewmont.Views.JoinStreamPage());
        }
    }
}
