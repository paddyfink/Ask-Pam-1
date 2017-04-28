using AskPam.Interfaces;
using AskPam.Models.Conversations;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace AskPam.Services
{
    public class ChatService : BaseService, IChatService
    {
        private readonly HubConnection _connection;
        private readonly IHubProxy _proxy;

        public event EventHandler<ConversationList> OnMessageReceived;

        public ChatService()
        {
            _connection = new HubConnection(ServiceHost, $"userId={UserId}&organizationId={OrgId}&platform=mobile" );
            _proxy = _connection.CreateHubProxy("ConversationHub");
        }
        
        public async Task ConnectAsync()
        {
            await _connection.Start();
            _proxy.On("newMessage", (ConversationList conversationList) => OnMessageReceived(this, conversationList));
        }

        public void Disconnect()
        {
            if (_connection.State != ConnectionState.Disconnected)
            {
                _connection.Stop();
            }                        
        }
        
    }
}
