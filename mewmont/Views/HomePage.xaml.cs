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

        void CreateStreamBtn_Pressed(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new mewmont.CreateStreamPage());
        }

        void JoinStreamBtn_Pressed(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new mewmont.JoinStreamPage());
        }
    }
}
