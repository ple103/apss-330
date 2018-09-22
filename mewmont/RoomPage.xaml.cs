using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mewmont
{
	public partial class RoomPage : ContentPage
	{
		public RoomPage()
		{
			InitializeComponent();
		}

        private void MessageEntry_Focused(object sender, FocusEventArgs e)
        {
            OptionButtons.IsVisible = false;
        }

        private void MessageEntry_Unfocused(object sender, FocusEventArgs e)
        {
            OptionButtons.IsVisible = true;
        }
    }
}
