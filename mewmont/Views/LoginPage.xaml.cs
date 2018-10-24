using System;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
            Permissions();
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

        public async void Permissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        status = results[Permission.Camera];
                }

                if (status == PermissionStatus.Granted)
                {
                    // Do something
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

                // Error
            }

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Microphone);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Microphone))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Microphone);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Microphone))
                        status = results[Permission.Microphone];
                }

                if (status == PermissionStatus.Granted)
                {
                    // Do something
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

                // Error
            }
        }
    }
}
