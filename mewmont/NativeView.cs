using System;
using Xamarin.Forms;

namespace VidyoConnector.Controls
{
    public class NativeView : View
    {
        public IntPtr Handle { get; set; }
        public float  Density { get; set; }

        public uint NativeWidth  { get { return (uint)(Width * Density); } }
        public uint NativeHeight { get { return (uint)(Height * Density); } }

        public NativeView() { Density = 1.0F; }
    }
}
