using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
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
    public sealed partial class BtAdsPage : Page
    {
        private BluetoothLEAdvertisementPublisher publisher;
        private BluetoothLEAdvertisementWatcher watcher;

        public BtAdsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Create and initialize a new publisher instance.
            publisher = new BluetoothLEAdvertisementPublisher();
            watcher = new BluetoothLEAdvertisementWatcher();
            watcher.Received += OnAdvertisementReceived;
        }

      async  private void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {

            // The timestamp of the event
            DateTimeOffset timestamp = eventArgs.Timestamp;

            // The type of advertisement
            BluetoothLEAdvertisementType advertisementType = eventArgs.AdvertisementType;

            // The received signal strength indicator (RSSI)
            Int16 rssi = eventArgs.RawSignalStrengthInDBm;

            // The local name of the advertising device contained within the payload, if any
            string localName = eventArgs.Advertisement.LocalName;

            // Check if there are any manufacturer-specific sections.
            // If there is, print the raw data of the first manufacturer section (if there are multiple).
            string manufacturerDataString = "";
            var manufacturerSections = eventArgs.Advertisement.ManufacturerData;
            if (manufacturerSections.Count > 0)
            {
                // Only print the first one of the list
                var manufacturerData = manufacturerSections[0];
                var data = new byte[manufacturerData.Data.Length];
                using (var reader = DataReader.FromBuffer(manufacturerData.Data))
                {
                    reader.ReadBytes(data);
                }
                // Print the company ID + the raw data in hex format
                manufacturerDataString = string.Format("0x{0}: {1}",
                    manufacturerData.CompanyId.ToString("X"),
                    BitConverter.ToString(data));
            }

            // Serialize UI update to the main UI thread
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                // Display these inf

                myTextBox.Text =  string.Format("[{0}]: type={1}, rssi={2}, name={3}, manufacturerData=[{4}]",
                    timestamp.ToString("hh\\:mm\\:ss\\.fff"),
                    advertisementType.ToString(),
                    rssi.ToString(),
                    localName,
                    manufacturerDataString);
            });
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (watcher != null)
            {
                watcher.Stop();
            }

            if (publisher != null)
            {
                publisher.Stop();
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            PublishInfo();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (publisher != null)
            {
                publisher.Stop();
            }
        }

        private void btnStartRcv_Click(object sender, RoutedEventArgs e)
        {
            Receive();
        }

        private void btnStopRcv_Click(object sender, RoutedEventArgs e)
        {
            watcher.Stop();
        }

        public void Receive()
        {

            // Create and initialize a new watcher instance.
            watcher = new BluetoothLEAdvertisementWatcher();
            var manufacturerData = new BluetoothLEManufacturerData();
            manufacturerData.CompanyId = 0xFFFE;

            // Finally set the data payload within the manufacturer-specific section
            // Here, use a 16-bit UUID: 0x1234 -> {0x34, 0x12} (little-endian)
            var writer = new DataWriter();
            writer.WriteUInt16(0x1234);
            // Make sure that the buffer length can fit within an advertisement payload. Otherwise you will get an exception.
            manufacturerData.Data = writer.DetachBuffer();

            // Add the manufacturer data to the advertisement filter on the watcher:
            watcher.AdvertisementFilter.Advertisement.ManufacturerData.Add(manufacturerData);

            watcher.SignalStrengthFilter.InRangeThresholdInDBm = -70;
            watcher.SignalStrengthFilter.OutOfRangeThresholdInDBm = -75;
            watcher.SignalStrengthFilter.OutOfRangeTimeout = TimeSpan.FromMilliseconds(2000);
            watcher.Received += OnAdvertisementReceived;
            watcher.Start();

        }


        public void PublishInfo()
        {
            

            var manufacturerData = new BluetoothLEManufacturerData();
            manufacturerData.CompanyId = 0xFFFE;

            // Finally set the data payload within the manufacturer-specific section
            // Here, use a 16-bit UUID: 0x1234 -> {0x34, 0x12} (little-endian)
            var writer = new DataWriter();
            UInt16 uuidData = 0x1234;
            writer.WriteUInt16(uuidData);
            writer.WriteString("HeyDude");

            manufacturerData.Data = writer.DetachBuffer();
            publisher.Advertisement.ManufacturerData.Add(manufacturerData);

            // Display the information about the published payload

            myTextBox.Text = string.Format("Published payload information: CompanyId=0x{0}, ManufacturerData=0x{1}",
                manufacturerData.CompanyId.ToString("X"),
                uuidData.ToString("X"));

            // Display the current status of the publisher
            tbStatus.Text = string.Format("Published Status: {0}, Error: {1}", publisher.Status, BluetoothError.Success);

            publisher.Start();
        }
    }
}
