using AskPam.Interfaces;
using System;
using System.Threading.Tasks;
using AskPam.Models.Conversations;
using AskPam.Models.Common;
using System.Net.Http;
using ModernHttpClient;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Xamarin.Forms;
using System.Collections.Generic;


[assembly: Dependency(typeof(AskPam.Services.ConversationService))]
namespace AskPam.Services
{
    public class ConversationService : BaseService, IConversationService
    {
        public async Task<PagedResult<ConversationList>> GetConversationsList(ConversationListRequest input)
        {
            using (var client = new HttpClient(new NativeMessageHandler()) { BaseAddress = new Uri(RequestUri) })
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                client.DefaultRequestHeaders.Add("Organization", OrgId);

                var data = JsonConvert.SerializeObject(input);
                var request = new StringContent(data);
                request.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (var response = await client.PostAsync("Conversations/GetConversationsList", request))
                {
                    using (var content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<PagedResult<ConversationList>>(json);

                        return result;
                    }
                }
            }
        }

        public async Task<Message> SendMessage(SendMessageRequest input)
        {
            using (var client = new HttpClient(new NativeMessageHandler()) { BaseAddress = new Uri(RequestUri) })
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                client.DefaultRequestHeaders.Add("Organization", OrgId);

                var data = JsonConvert.SerializeObject(input);
                var request = new StringContent(data);
                request.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (var response = await client.PostAsync("Conversations/SendMessage", request))
                {
                    using (var content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<Message>(json);

                        return result;
                    }
                }
            }
        }

        public async Task<Conversation> GetConversation(int conversationId)
        {
            using (var client = new HttpClient(new NativeMessageHandler()) { BaseAddress = new Uri(RequestUri) })
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token); 
                client.DefaultRequestHeaders.Add("Organization", OrgId); 

                using (var response = await client.GetAsync($"Conversations/GetConversation?conversationId={conversationId}"))
                {
                    using (var content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<Conversation>(json);

                        return result;
                    }
                }
            }
        }

        public async Task<IList<EnumValue>> GetFilters()
        {
            using (var client = new HttpClient(new NativeMessageHandler()) { BaseAddress = new Uri(RequestUri) })
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token); 
                client.DefaultRequestHeaders.Add("Organization", OrgId); 

                using (var response = await client.GetAsync("Conversations/GetFilters"))
                {
                    using (var content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<IList<EnumValue>>(json);

                        return result;
                    }
                }
            }
        }

        public async Task AssignToUser(AssignConversationRequest input)
        {
            using (var client = new HttpClient(new NativeMessageHandler()) { BaseAddress = new Uri(RequestUri) })
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                client.DefaultRequestHeaders.Add("Organization", OrgId);

                var data = JsonConvert.SerializeObject(input);
                var request = new StringContent(data);
                request.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await client.PostAsync("Conversations/AssignToUser", request);
            }
        }

        public async Task EnableBot(int conversationId)
        {
            using (var client = new HttpClient(new NativeMessageHandler()) { BaseAddress = new Uri(RequestUri) })
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                client.DefaultRequestHeaders.Add("Organization", OrgId);

                var data = JsonConvert.SerializeObject(conversationId);
                var request = new StringContent(data);
                request.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await client.PostAsync("Conversations/EnableBot", request);
            }
        }
    }
}
