using AskPam.Models.Common;
using AskPam.Models.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskPam.Interfaces
{
    public interface IConversationService
    {
        Task<PagedResult<ConversationList>> GetConversationsList(ConversationListRequest input);
        Task<IList<EnumValue>> GetFilters();
        Task<Conversation> GetConversation(int conversationId);
        Task<Message> SendMessage(SendMessageRequest input);
        Task AssignToUser(AssignConversationRequest input);
        Task EnableBot(int conversationId);
    }
}
