using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Win10DemoKing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DropPage : Page
    {
        public DropPage()
        {
            this.InitializeComponent();
        }

        private async void OnDropped(object sender, DragEventArgs e)
        {
            var file = (StorageFile)(await e.DataView.GetStorageItemsAsync())[0];
            var imageSource = new BitmapImage();
            imageSource.SetSource(await file.OpenReadAsync());
            myImg.Source = imageSource;
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            myGrid.Background = new SolidColorBrush(Colors.Red);
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            myGrid.Background = new SolidColorBrush(Colors.Gray);
        }
    }
}
