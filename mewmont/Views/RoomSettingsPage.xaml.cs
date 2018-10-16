using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mewmont
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoomSettingsPage : ContentPage
	{
		public RoomSettingsPage()
		{
			InitializeComponent ();
		}

        private void MediaChangeBtn_OnClick(object sender, EventArgs e)
        {
            App.RoomManager.SetRoomMedia(MediaUrlEntry.Text);
            Navigation.PopAsync();
        }
    }
}
