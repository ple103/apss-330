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

        void OnStreamSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var room = e.SelectedItem as Room;
            var roomPage = new RoomPage(room.Id);
            Navigation.PushAsync(roomPage);
        }

        private void PrivateBtn_Pressed(object sender, EventArgs e)
        {
            var joinPrivateStreamPage = new JoinPrivateStreamPage();
            Navigation.PushAsync(joinPrivateStreamPage);
        }
    }
}
