using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mewmont
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void LoginBtn_Pressed(object sender, EventArgs e)
        {
            await App.UserManager.Login(UsernameEntry.Text, PasswordEntry.Text);
            PasswordEntry.Text = "";
            if (App.UserManager.User.Token == null)
            {
                await DisplayAlert("Error", "Failed to log in. Please check your login details", "OK");
            }
            else
            {
                await Navigation.PushAsync(new HomePage());
            }
        }

        private async void RegisterBtn_Pressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private void SkipBtn_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
    }
}
