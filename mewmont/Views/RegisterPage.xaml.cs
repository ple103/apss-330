using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mewmont.Models.SocketSenders;

namespace mewmont
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
		}

        private async void RegisterBtn_Pressed(object sender, EventArgs e)
        {
            RegistrationResponse response = await App.UserManager.Register(UsernameEntry.Text, PasswordEntry.Text);
            if (response == null || !response.success)
            {
                await DisplayAlert("Error", "Please enter different details and try again.", "OK");
            }
            else
            {
                await DisplayAlert("Account Created", "You're account has successfully been created, please go back and login", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
