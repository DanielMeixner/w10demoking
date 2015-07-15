using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.WiFi;
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

namespace Win10DemoKing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class wifi : Page
    {
        private WiFiAdapter nwAdapter;

        public wifi()
        {
            this.InitializeComponent();
        }

        public async void Scan()
        {
            var access = await WiFiAdapter.RequestAccessAsync();

            var result = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
            if (result.Count >= 1)
            {
                 nwAdapter = await WiFiAdapter.FromIdAsync(result[0].Id);
                await nwAdapter.ScanAsync();

                sp.Children.Clear();
                foreach (var item in nwAdapter.NetworkReport.AvailableNetworks)
                {
                    Button b = new Button();
                    b.Content += item.Ssid;
                    b.Click += B_Click;
                    sp.Children.Add(b);                    

                }

        
                
                

            }
        }

        private async void B_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string ssid = b.Content as string;
            var nw = nwAdapter.NetworkReport.AvailableNetworks.Where(y => y.Ssid.ToLower() == ssid).FirstOrDefault();
            await nwAdapter.ConnectAsync(nw, WiFiReconnectionKind.Automatic);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Scan();
        }
    }
}
