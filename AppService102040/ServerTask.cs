using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;

namespace ServerTaskComponent102040
{
    public sealed class ServerTask : IBackgroundTask
    {

        private BackgroundTaskDeferral serviceDeferral;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            //Take a service deferral so the service isn't terminated
            serviceDeferral = taskInstance.GetDeferral();

            // Associate a cancellation handler with the background task.
            taskInstance.Canceled += TaskInstance_Canceled;

            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;

            if (triggerDetails != null)
            {
                //Listen for incoming app service requests
                triggerDetails.AppServiceConnection.RequestReceived += OnRequestReceived;
            }
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            //throw new NotImplementedException();
        }

        private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {

            var message = args.Request.Message;
            string cmd = message["cmd"] as string;
            string txt = message["txt"] as string;


            var messageDeferral = args.GetDeferral();

            string result = txt.ToUpper();
            var returnMessage = new ValueSet();
            returnMessage.Add("result", result);
            var responseStatus = await args.Request.SendResponseAsync(returnMessage);

            messageDeferral.Complete();

        }
    }


}

