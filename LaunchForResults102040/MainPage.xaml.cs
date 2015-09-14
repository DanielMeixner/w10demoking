using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LaunchForResults102040
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Debug.WriteLine(GetPackageFamilyName());
        }

        private string GetPackageFamilyName()
        {
            return Windows.ApplicationModel.Package.Current.Id.FamilyName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProtocolForResultsOperation ro = ((App)App.Current).mem["ResultsOperation"] as ProtocolForResultsOperation;

            // wait for user input oder simply return a value
            var resultData = new ValueSet();
            resultData.Add("Result", "Hello from Result Target");
            ro.ReportCompleted(resultData);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var options = new LauncherOptions();
            options.TargetApplicationPackageFamilyName = "566f2a0b-0595-4e55-95b0-80d48e0e6e66_e7qah2kqbxs60";
            var res = await Windows.System.Launcher.LaunchUriForResultsAsync(new Uri("king:/key=val"), options);

            var msg = res.Result["Result"] as string;
            await new MessageDialog(msg).ShowAsync();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var options = new LauncherOptions();

            options.TargetApplicationPackageFamilyName = "8ee0e70c-1542-4a8a-b8c1-01b63c197d86_e7qah2kqbxs60";
            var res = await Launcher.LaunchUriForResultsAsync(new Uri("demoking102040:/key=val"), options);

            var msg = res.Result["Result"] as string;
            await new MessageDialog(msg).ShowAsync();
        }
    }
}
