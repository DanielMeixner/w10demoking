using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Windows10DemoKing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        public MapPage()
        {
            this.InitializeComponent();

            NewMethod();

        }

        private async void NewMethod()
        {
            BasicGeoposition b;

            var accessStatus = await Geolocator.RequestAccessAsync();

            if (accessStatus == GeolocationAccessStatus.Allowed)
            {
                Geolocator geolocator = new Geolocator();
                Geoposition pos = await geolocator.GetGeopositionAsync();
                b = new BasicGeoposition();
                b.Latitude = pos.Coordinate.Point.Position.Latitude;
                b.Longitude = pos.Coordinate.Point.Position.Longitude;
            }
            else
            { 

                b = new BasicGeoposition();
                b.Latitude = 48.279301;
                b.Longitude = 11.582600;
            }

            Geopoint g = new Geopoint(b);
            MyMapControl.Center = g;
            MyMapControl.ZoomLevel = 13;
        }
    }
}
