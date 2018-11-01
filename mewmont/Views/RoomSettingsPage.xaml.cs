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

        /// <summary>
        /// Change the media instantly, and return the room page upon changing media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaChangeBtn_OnClick(object sender, EventArgs e)
        {
            App.RoomManager.SetRoomMedia(MediaUrlEntry.Text);
            Navigation.PopAsync();
        }

        /// <summary>
        /// Open the current media in the YouTube app and close the settings page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaExternalBtn_OnClick(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("vnd.youtube:" + App.RoomManager.Room.CurrentMedia.mediaId));
            Navigation.PopAsync();
        }
    }
}
