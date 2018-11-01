using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mewmont.Data;
using VidyoConnector;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace mewmont
{
	public partial class App : Application
	{
        public static IVidyoController _vidyoController = null;

        public static int ScreenHeight { get; set; }
        public static int ScreenWidth { get; set; }

        public static RoomManager RoomManager { get; private set; }
        public static UserManager UserManager { get; private set; }

        public static IRestService restService;

        public App (IVidyoController vidyoController)
		{
			InitializeComponent();
            restService = new RestService();
            RoomManager = new RoomManager(new WebSocketService());
            UserManager = new UserManager();
            MainPage = new NavigationPage(new LoginPage());
            _vidyoController = vidyoController;
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
