using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mewmont.Models;

namespace mewmont
{
	public partial class JoinStreamPage : ContentPage
	{
		public JoinStreamPage ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            RoomList.ItemsSource = await App.RoomManager.GetRoomsData();
        }

        /// <summary>
        /// When a room is selected, create a new room with the ID of the selected room.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnStreamSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var room = e.SelectedItem as Room;
            var roomPage = new RoomPage(room.Id);
            Navigation.PushAsync(roomPage);
        }

        /// <summary>
        /// Go to the private room page if the button was pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrivateBtn_Pressed(object sender, EventArgs e)
        {
            var joinPrivateStreamPage = new JoinPrivateStreamPage();
            Navigation.PushAsync(joinPrivateStreamPage);
        }
    }
}
