using AskPam.Interfaces;
using AskPam.Models.Users;
using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AskPam.Services.UserService))]
namespace AskPam.Services
{
    public class UserService : BaseService, IUserService
    {

        public async Task<List<UserList>> GetAllUsers()
        {

            using (var client = new HttpClient(new NativeMessageHandler()) { BaseAddress = new Uri(RequestUri) })
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                client.DefaultRequestHeaders.Add("Organization", OrgId);

                using (var response = await client.GetAsync("User/GetAllUsers"))
                {
                    using (var content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<List<UserList>>(json);

                        return result;
                    }
                }
            }            
        }
    }
}
