using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Popups;
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
    public sealed partial class LaunchUri : Page
    {
        public LaunchUri()
        {
            this.InitializeComponent();
        }

        private async void btnLaunchUri_Click(object sender, RoutedEventArgs e)
        {
            string uriString = "demoking102040:key=value123";
            await Launcher.LaunchUriAsync(new Uri(uriString));
        }
        private async void btnLaunchUriSpecific_Click(object sender, RoutedEventArgs e)
        {
            string uriString = "demoking102040:key=value123";
            var opt = new LauncherOptions();

            // PFN
            opt.TargetApplicationPackageFamilyName = "5082b433-03c4-4db0-abf6-d2d63493f851_e7qah2kqbxs60";

            // token
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///images/DMX.jpg"));
            string tok = SharedStorageAccessManager.AddFile(file);
            uriString = uriString += "&file=" + tok;

            await Launcher.LaunchUriAsync(new Uri(uriString),opt);
        }

        private async void btnQueryLaunchInfo_Click(object sender, RoutedEventArgs e)
        {
            string uriString = "demoking102040:";
            var uri = new Uri(uriString);
            var res = await Launcher.QueryUriSupportAsync(uri, LaunchQuerySupportType.Uri);

            if (res == LaunchQuerySupportStatus.Available)
            {
                await new MessageDialog("Yeah!").ShowAsync();
            }
            else 
            {
                await new MessageDialog("Sorry, dude - not installed").ShowAsync();
            }
        }

        private async void btnFindAlHandlers_Click(object sender, RoutedEventArgs e)
        {
            // doesnt work!
            //var providers = await Launcher.FindUriSchemeHandlersAsync("demoking");
        }
    }
}
