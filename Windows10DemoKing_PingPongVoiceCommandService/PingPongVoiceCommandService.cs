using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Resources.Core;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;

namespace Windows10DemoKing_VoiceCommandService
{
    public sealed class PingPongVoiceCommandService : IBackgroundTask
    {
        VoiceCommandServiceConnection voiceServiceConnection;
        BackgroundTaskDeferral serviceDeferral;
        private HttpStatusCode code;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            serviceDeferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnTaskCanceled;

            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;

            // This should match the uap:AppService and VoiceCommandService references from the 
            // package manifest and VCD files, respectively. Make sure we've been launched by
            // a Cortana Voice Command.
            if (triggerDetails.Name == "PingPongVoiceCommandService102040")
            {
                try
                {
                    voiceServiceConnection = VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);
                    voiceServiceConnection.VoiceCommandCompleted += OnVoiceCommandCompleted;

                    VoiceCommand voiceCommand = await voiceServiceConnection.GetVoiceCommandAsync();


                    switch (voiceCommand.CommandName)
                    {
                        case "sendPingPong":
                            var t = voiceCommand.Properties["type"][0];
                            await DoItAsync(t);
                            await SendCompletionMessageForDestination(t + $" ({code})");
                            break;

                        default:
                            LaunchAppInForeground();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Handling Voice Command failed " + ex.ToString());
                }
                finally
                {
                    CleanUpDeferral();
                }

            }
        }

        private async Task DoItAsync(string t)
        {

            await SendProgressState(1);

         //   await DoHttpRequest();

            await DoBTStuff(t);
            await SendProgressState(4);
        }

        private async Task DoBTStuff(string t)
        {
            BluetoothLEAdvertisementPublisher publisher = new BluetoothLEAdvertisementPublisher();
            //var manufacturerData = new BluetoothLEManufacturerData();
            //manufacturerData.CompanyId = 0xFFFE;
            //var writer = new DataWriter();
            //writer.WriteString($"Message sent: {t} ");
            //manufacturerData.Data = writer.DetachBuffer();
            //publisher.Advertisement.ManufacturerData.Add(manufacturerData);
            //publisher.Start();
            //await Task.Delay(500);
            //await SendProgressState(3);
            //publisher.Stop();


            var manufacturerData = new BluetoothLEManufacturerData();
            manufacturerData.CompanyId = 0xFFFE;

            // Finally set the data payload within the manufacturer-specific section
            // Here, use a 16-bit UUID: 0x1234 -> {0x34, 0x12} (little-endian)
            var writer = new DataWriter();
            UInt16 uuidData = 0x1234;
            writer.WriteUInt16(uuidData);
            writer.WriteString("Beacon from Cortana");
            
            manufacturerData.Data = writer.DetachBuffer();
            publisher.Advertisement.ManufacturerData.Add(manufacturerData);
            publisher.Start();
            await Task.Delay(3000);
            await SendProgressState(3);
            publisher.Stop();
        }

        private async Task DoHttpRequest()
        {
            HttpClient cl = new HttpClient();
            var resp = await cl.GetAsync(@"https://msdn.microsoft.com/de-de/library/windows/apps/xaml/dn974228.aspx");
            code = resp.StatusCode;
            await SendProgressState(2);
        }

        private async Task SendProgressState(int i)
        {
            var userProgressMessage = new VoiceCommandUserMessage();
            userProgressMessage.DisplayMessage = userProgressMessage.SpokenMessage = "Sending Beacons ... " + i;


            VoiceCommandResponse response = VoiceCommandResponse.CreateResponse(userProgressMessage);
            await voiceServiceConnection.ReportProgressAsync(response);
        }

        private async void LaunchAppInForeground()
        {
            var userMessage = new VoiceCommandUserMessage();
            userMessage.SpokenMessage = "Launching Demo King";

            var response = VoiceCommandResponse.CreateResponse(userMessage);            
            response.AppLaunchArgument = "key=val";

            await voiceServiceConnection.RequestAppLaunchAsync(response);
        }


        private async Task SendCompletionMessageForDestination(string v)
        {
            VoiceCommandUserMessage um = new VoiceCommandUserMessage();
            string msg = $"I'm done! I sent a {v}";
            um.SpokenMessage = msg;
            um.DisplayMessage = msg;

            var destinationsContentTiles = new List<VoiceCommandContentTile>();

            var destinationTile = new VoiceCommandContentTile();
            destinationTile.ContentTileType =  VoiceCommandContentTileType.TitleWith68x68IconAndText;


            var response = VoiceCommandResponse.CreateResponse(um, destinationsContentTiles);

            await voiceServiceConnection.ReportSuccessAsync(response);
        }

        private void OnVoiceCommandCompleted(VoiceCommandServiceConnection sender, VoiceCommandCompletedEventArgs args)
        {
            CleanUpDeferral();
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            CleanUpDeferral();
        }

        private void CleanUpDeferral()
        {
            if (this.serviceDeferral != null)
            {
                this.serviceDeferral.Complete();
            }
        }
    }

}
