using System;
using System.ComponentModel;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using VidyoConnector;
using Xamarin.Forms;

namespace mewmont
{
    public partial class VidyoPage : ContentPage
    {
        IVidyoController _vidyoController = null;
        VidyoViewModel _viewModel = VidyoViewModel.GetInstance();
        double _pageWidth = 0;
        double _pageHeight = 0;
        bool _hideConfig = false;
        bool _allowReconnect = true;

        public VidyoPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
            Permissions();
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

        public void Initialize(IVidyoController vidyoController)
        {
            // Provide the videoView to the Vidyo controller
            vidyoController.SetNativeView(_videoView);
            _vidyoController = vidyoController;

            var i = (INotifyPropertyChanged)_vidyoController;
            i.PropertyChanged += new PropertyChangedEventHandler(VidyoControllerPropertyChanged);
        }

        void VidyoControllerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ConnectorState")
            {
                VidyoConnectorState = _vidyoController.ConnectorState;
            }
        }

        void OnConnectButtonClicked(object sender, EventArgs args)
        {
            if (_viewModel.CallAction == VidyoCallAction.VidyoCallActionConnect)
            {
                _viewModel.ToolbarStatus = "Connecting...";

                if (!_vidyoController.Connect(_viewModel.Host, _viewModel.Token, _viewModel.DisplayName, _viewModel.ResourceId))
                {
                    VidyoConnectorState = VidyoConnectorState.VidyoConnectorStateConnectionFailure;
                }
                else
                {
                    _viewModel.CallAction = VidyoCallAction.VidyoCallActionDisconnect;

                    // Display the spinner animation
                    _connectionSpinner.IsRunning = true;
                }
            }
            else
            {
                _viewModel.ToolbarStatus = "Disconnecting...";
                _vidyoController.Disconnect();
            }
        }

        void OnCameraPrivacyButtonClicked(object sender, EventArgs args)
        {
            _vidyoController.SetCameraPrivacy(_viewModel.ToggleCameraPrivacy());
        }

        void OnMicrophonePrivacyButtonClicked(object sender, EventArgs args)
        {
            _vidyoController.SetMicrophonePrivacy(_viewModel.ToggleMicrophonePrivacy());
        }

        void OnCycleCameraButtonClicked(object sender, EventArgs args)
        {
            _vidyoController.CycleCamera();
        }

        void DiagnosticsButtonClicked(object sender, EventArgs args)
        {
            _viewModel.DisplayDiagnostics = !_viewModel.DisplayDiagnostics;
            if (_viewModel.DisplayDiagnostics)
            {
                ClientVersionLabel.IsVisible = true;
                _vidyoController.EnableDebugging();
            }
            else
            {
                ClientVersionLabel.IsVisible = false;
                _vidyoController.DisableDebugging();
            }
        }

        VidyoConnectorState VidyoConnectorState
        {
            set
            {
                // UI updates should be made on main thread
                Device.BeginInvokeOnMainThread(() =>
                {
                    // Update the toggle connect button to either start call or end call image
                    _viewModel.CallAction = value == VidyoConnectorState.VidyoConnectorStateConnected ?
                        VidyoCallAction.VidyoCallActionDisconnect : VidyoCallAction.VidyoCallActionConnect;

                    // Set the status text in the toolbar
                    _viewModel.ToolbarStatus = VidyoDefs.StateDescription[value];

                    if (value == VidyoConnectorState.VidyoConnectorStateConnected)
                    {
                        if (!_hideConfig)
                        {
                            // Update the view to hide the form
                            _form.IsVisible = false;
                        }
                    }
                    else
                    {
                        // VidyoConnector is disconnected

                        // If the allow-reconnect flag is set to false and a normal (non-failure) disconnect occurred,
                        // then disable the toggle connect button, in order to prevent reconnection.
                        if (!_allowReconnect && (value == VidyoConnectorState.VidyoConnectorStateDisconnected))
                        {
                            _toggleConnectButton.IsEnabled = false;
                            _viewModel.ToolbarStatus = "Call ended";
                        }

                        if (!_hideConfig)
                        {
                            // Update the view to display the controls
                            _form.IsVisible = true;
                        }
                    }

                    // Hide the spinner animation
                    _connectionSpinner.IsRunning = false;
                });
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (Math.Abs(width - _pageWidth) > 0.001 || Math.Abs(height - _pageHeight) > 0.001)
            {
                _pageWidth = width;
                _pageHeight = height;
                if (_vidyoController != null)
                    _vidyoController.OnOrientationChange();
            }
        }
    }
}
