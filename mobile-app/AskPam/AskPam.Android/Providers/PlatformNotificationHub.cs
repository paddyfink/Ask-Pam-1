using Android.Util;
using AskPam.Common;
using AskPam.Interfaces;
using Microsoft.Azure.Mobile.Analytics;
using System;
using System.Collections.Generic;
using WindowsAzure.Messaging;
using Xamarin.Forms;

[assembly: Dependency(typeof(AskPam.Droid.Providers.PlatformNotificationHub))]
namespace AskPam.Droid.Providers
{
    public class PlatformNotificationHub : INotificationHub
    {
        private string _registrationId;

        public void RegisterNotification(string registrationId)
        {
            _registrationId = (!string.IsNullOrEmpty(registrationId) 
                ? registrationId 
                : PushHandlerService.RegistrationID);

            var hub = new NotificationHub(NotificationHubKeys.NotificationHubName,
                                        NotificationHubKeys.ListenConnectionString,
                                        Android.App.Application.Context);
            try
            {
                hub.UnregisterAll(_registrationId);
            }
            catch (Exception ex)
            {
                Log.Error(AskPamBroadcastReceiver.TAG, ex.Message);
            }

            try
            {
                PlatformAccountStoreProvider accountStore = new PlatformAccountStoreProvider();

                var tags = new List<string>();
                if (accountStore != null)
                {
                    tags.Add(accountStore.AccountProperties[AccountStoreKeys.UserIdKey]);
                    tags.Add(accountStore.AccountProperties[AccountStoreKeys.OrgIdKey]);
                }

                var hubRegistration = hub.Register(_registrationId, tags.ToArray());
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("RegisterNotification Failed", new Dictionary<string, string> { { "Source", "Message" }, { e.Source, e.Message } });
            }
        }
        
    }
}
