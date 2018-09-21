using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace mewmont
{
	public partial class App : Application
	{
        public static int ScreenHeight { get; set; }
        public static int ScreenWidth { get; set; }

        public App ()
		{
			InitializeComponent();

			MainPage = new RoomPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
