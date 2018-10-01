using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mewmont.Data;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace mewmont
{
	public partial class App : Application
	{
        public static int ScreenHeight { get; set; }
        public static int ScreenWidth { get; set; }
        public static RoomManager RoomManager { get; private set; }

        public App ()
		{
			InitializeComponent();

            RoomManager = new RoomManager(new RestService(), new WebSocketService());
            MainPage = new NavigationPage(new RoomPage());
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
