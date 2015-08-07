using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class App2AppPage : Page
    {
        private AppServiceConnection connection;

        public App2AppPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CallAppService();
        }

        private async void CallAppService()
        {
            connection = new AppServiceConnection();
            connection.AppServiceName = "demoqueenservice";
            connection.PackageFamilyName = "8d346270-b487-4d4d-9b45-8af14df2b34a_e7qah2kqbxs60";
            AppServiceConnectionStatus connectionStatus = await connection.OpenAsync();

            var message = new ValueSet();
            message.Add("cmd", "toUpper");
            message.Add("txt", tb.Text);            
            
            AppServiceResponse response = await connection.SendMessageAsync(message);
            if (response.Status == AppServiceResponseStatus.Success)
            {
                var res = response.Message["result"] as string;
                new MessageDialog(res).ShowAsync();
            }

            connection.Dispose();
            connection = null;
        }

       
    }
}
