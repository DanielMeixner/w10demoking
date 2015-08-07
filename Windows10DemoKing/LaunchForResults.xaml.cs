using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class LaunchForResultsPage : Page
    {
        public LaunchForResultsPage()
        {
            this.InitializeComponent();
        }

        private async void btnLaunch_Click(object sender, RoutedEventArgs e)
        {
            var options = new LauncherOptions();
            options.TargetApplicationPackageFamilyName = "5082b433-03c4-4db0-abf6-d2d63493f851_e7qah2kqbxs60";
            var res = await Windows.System.Launcher.LaunchUriForResultsAsync(
                new Uri("demoking102040:/key=val"), options);
            
            var msg = res.Result["Result"] as string;
            await new MessageDialog(msg).ShowAsync();

        }

        private void btnLaunchSpecific_Click(object sender, RoutedEventArgs e)
        {
            LaunchSpecific();

        }

        private static async void LaunchSpecific()
        {
            var options = new LauncherOptions();
            options.TargetApplicationPackageFamilyName = "23d8f929-1d97-426a-9f07-67babe333ec6_e7qah2kqbxs60";
            var uri = new Uri("");
            await Launcher.LaunchUriAsync(uri, options);
        }
    }
}
