using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AskPam.Interfaces;
using AskPam.Models.Authorization;
using Newtonsoft.Json;
using System.Net.Http;
using ModernHttpClient;
using Xamarin.Forms;
using System.Net.Http.Headers;
using Microsoft.Azure.Mobile.Analytics;

[assembly: Dependency(typeof(AskPam.Services.AuthenticationService))]
namespace AskPam.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public async Task<AuthInfo> Login(Login login)
        {
            try
            {
                using (var client = new HttpClient(new NativeMessageHandler()) { BaseAddress = new Uri(RequestUri) })
                {
                    var data = JsonConvert.SerializeObject(login);
                    var request = new StringContent(data);
                    request.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    using (var response = await client.PostAsync("Account/Login", request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (var content = response.Content)
                            {
                                string json = await content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<AuthInfo>(json);
                                return result;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("Authentication Service Failed", new Dictionary<string, string> { { "Source", "Message" }, { e.Source, e.Message} });
            }

            return null;
        }
    }
}
