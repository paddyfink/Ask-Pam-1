using AskPam.Models.Conversations;
using System;
using System.Threading.Tasks;

namespace AskPam.Interfaces
{
    public interface IChatService
    {
        Task ConnectAsync();

        void Disconnect();

        event EventHandler<ConversationList> OnMessageReceived;
    }
}
