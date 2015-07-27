using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;

namespace Win10DemoKing
{
    public class CustomNetworkTrigger :StateTriggerBase
    {
        public  CustomNetworkTrigger()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
        }

        private async void NetworkInformation_NetworkStatusChanged(object sender)
        {
            //var prof = NetworkInformation.GetInternetConnectionProfile();
            //if (prof != null)
            //{
            //    var dispatcher = Dispatcher;
            //    if (dispatcher != null)
            //    {
            //        await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            //         {
            //             SetActive(prof.GetNetworkConnectivityLevel().HasFlag(RequiredLevel));

            //         });
            //    }
            //}
        }

        public NetworkConnectivityLevel RequiredLevel { get; set; }
    }
}
