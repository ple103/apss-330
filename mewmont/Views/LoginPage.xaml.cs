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

        /// <summary>
        /// Attempt to login with the credidentials provided
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoginBtn_Pressed(object sender, EventArgs e)
        {
            await App.UserManager.Login(UsernameEntry.Text, PasswordEntry.Text);
            // Clear the password for security
            PasswordEntry.Text = "";

            // If the user wasn't returned, then it was an unsuccessful login.
            if (App.UserManager.User.Token == null)
            {
                await DisplayAlert("Error", "Failed to log in. Please check your login details", "OK");
            }
            else
            {
                // Proceed to the HomePage if the login was successful.
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

        // Ask the user for the permissions required for video chat, at the start to prevent disruption before entering the room.
        public async void Permissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        await DisplayAlert("Camera", "Video chat will not work without camera permissions.", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        status = results[Permission.Camera];
                }

                if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Camera", "Video chat will not work without camera permissions.", "OK");
                }
            }
            catch (Exception ex)
            {

            }

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Microphone);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Microphone))
                    {
                        await DisplayAlert("Microphone", "Video chat will not work without microphone permissions.", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Microphone);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Microphone))
                        status = results[Permission.Microphone];
                }

                if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Microphone", "Video chat will not work without microphone permissions.", "OK");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
