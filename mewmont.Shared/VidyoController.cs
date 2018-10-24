using System;
using System.ComponentModel;
using VidyoClient;
using Xamarin.Forms;
#if __ANDROID__
using Android.App;
#endif

namespace VidyoConnector
{
    public class VidyoController : IVidyoController, INotifyPropertyChanged, Connector.IConnect, Connector.IRegisterLogEventListener, Connector.IRegisterLocalCameraEventListener
    {
        private static VidyoController _instance = new VidyoController();
        public static VidyoController GetInstance() { return _instance; }
        private VidyoController() { }

        // Private members
        Connector _connector = null;
        bool _vidyoClientInitialized = false;
        bool _enableDebug = false;
        private bool _userSetPrivacy = false;
        string _experimentalOptions = null;
        Logger _logger = Logger.GetInstance();
        VidyoConnectorState _state;

        // Public properties
        public Controls.NativeView _videoView;

        public event PropertyChangedEventHandler PropertyChanged;

        public VidyoConnectorState ConnectorState
        {
            get { return _state; }
            set
            {
                _state = value;
                // Raise PropertyChanged event
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ConnectorState"));
            }
        }

        public void SetNativeView(Controls.NativeView videoView)
        {
            _videoView = videoView;
        }

        public string OnAppStart()
        {
            string clientVersion = "Failed";
#if __ANDROID__
            ConnectorPKG.SetApplicationUIContext(Forms.Context as Activity);
#endif
            // Initialize VidyoClient library.
            // This should be called only once throughout the lifetime of the app.
            _vidyoClientInitialized = ConnectorPKG.Initialize();

            if (_vidyoClientInitialized)
            {
                try
                {
                    _connector = new Connector(_videoView.Handle,
                                               Connector.ConnectorViewStyle.ConnectorviewstyleDefault,
                                               15,
                                               "info@VidyoClient info@VidyoConnector warning",
                                               "",
                                               0);

                    // Get the version of VidyoClient
                    clientVersion = _connector.GetVersion();

                    // If enableDebug is configured then enable debugging
                    if (_enableDebug)
                    {
                        _connector.EnableDebug(7776, "warning info@VidyoClient info@VidyoConnector");
                    }

                    // Set experimental options if any exist
                    if (_experimentalOptions != null)
                    {
                        ConnectorPKG.SetExperimentalOptions(_experimentalOptions);
                    }

                    // Set initial position
                    RefreshUI();

                    _connector.RegisterLocalCameraEventListener(this);

                    // Register for log callbacks
                    if (!_connector.RegisterLogEventListener(this, "info@VidyoClient info@VidyoConnector warning"))
                    {
                        _logger.Log("VidyoConnector RegisterLogEventListener failed");
                    }
                }
                catch (Exception e)
                {
                    _logger.Log("VidyoConnector Construction failed");
                    _logger.Log(e.Message);
                }
            }
            else
            {
                _logger.Log("ERROR: VidyoClientInitialize failed - not constructing VidyoConnector ...");
            }
            return clientVersion;
        }

        public void OnAppSleep()
        {
            if (_connector != null)
            {
                //IOS disables the camera when applications are sent to the background 
                //see https://stackoverflow.com/questions/38278216/has-apple-officially-not-allowed-access-to-camera-for-app-in-background
                //to prevent the camera from freezing when the app moves into the backround set privacy to true
#if __IOS__
                _connector.SetCameraPrivacy(true);
#endif
                _connector.SetMode(Connector.ConnectorMode.ConnectormodeBackground);
            }
        }

        public void OnAppResume()
        {
            if (_connector != null)
            {
                //IOS disables the camera when applications are sent to the background 
                //see https://stackoverflow.com/questions/38278216/has-apple-officially-not-allowed-access-to-camera-for-app-in-background
                //when the app re-enters the foreground set the privacy back to it's previous setting
#if __IOS__
                _connector.SetCameraPrivacy(this._userSetPrivacy);
#endif
                _connector.SetMode(Connector.ConnectorMode.ConnectormodeForeground);
            }
        }

        public void Cleanup()
        {
            _connector.Disable();
            _connector = null;

            // Uninitialize VidyoClient library.
            // This should be called only once throughout the lifetime of the app.
            ConnectorPKG.Uninitialize();
        }

        public bool Connect(string host, string token, string displayName, string resourceId)
        {
            return _connector.Connect(host, token, displayName, resourceId, this);
        }

        public void Disconnect()
        {
            _connector.Disconnect();
        }

        // Set the microphone privacy
        public void SetMicrophonePrivacy(bool privacy)
        {
            _connector.SetMicrophonePrivacy(privacy);
        }

        // Set the camera privacy
        public void SetCameraPrivacy(bool privacy)
        {
            this._userSetPrivacy = privacy;
            _connector.SetCameraPrivacy(privacy);
        }

        // Cycle the camera
        public void CycleCamera()
        {
            _connector.CycleCamera();
        }

        // Orientation has changed so refresh the UI
        public void OnOrientationChange()
        {
            this.RefreshUI();
        }

        /*
         * Private Utility Functions
         */

        // Refresh the UI
        public void RefreshUI()
        {
            // Refresh the rendering of the video
            if (_connector != null)
            {
                uint w = _videoView.NativeWidth;
                uint h = _videoView.NativeHeight;
                _connector.ShowViewAt(_videoView.Handle, 0, 0, w, h);
                _logger.Log("VidyoConnectorShowViewAt: x = 0, y = 0, w = " + w + ", h = " + h);
            }
        }

        public void OnSuccess()
        {
            _logger.Log("OnSuccess");
            ConnectorState = VidyoConnectorState.VidyoConnectorStateConnected;
        }

        public void OnFailure(Connector.ConnectorFailReason reason)
        {
            _logger.Log("OnFailure");
            ConnectorState = VidyoConnectorState.VidyoConnectorStateConnectionFailure;
        }

        public void OnDisconnected(Connector.ConnectorDisconnectReason reason)
        {
            _logger.Log("OnDisconnected");
            ConnectorState = (reason == Connector.ConnectorDisconnectReason.ConnectordisconnectreasonDisconnected) ?
                VidyoConnectorState.VidyoConnectorStateDisconnected : VidyoConnectorState.VidyoConnectorStateDisconnectedUnexpected;
        }

        public void OnLog(LogRecord logRecord)
        {
            _logger.LogClientLib(logRecord.message);
        }

        public void OnLocalCameraAdded(LocalCamera localCamera)
        {
            _logger.Log("OnLocalCameraAdded");
        }

        public void OnLocalCameraRemoved(LocalCamera localCamera)
        {
            _logger.Log("OnLocalCameraRemoved");
        }

        public void OnLocalCameraSelected(LocalCamera localCamera)
        {
            _logger.Log("OnLocalCameraSelected");
        }

        public void OnLocalCameraStateUpdated(LocalCamera localCamera, VidyoClient.Device.DeviceState state)
        {
            _logger.Log("OnLocalCameraStateUpdated");
        }
        public void EnableDebugging()
        {
            _connector.EnableDebug(7776, "warning info@VidyoClient info@VidyoConnector");
        }
        public void DisableDebugging()
        {
            _connector.DisableDebug();
        }
    }
}
