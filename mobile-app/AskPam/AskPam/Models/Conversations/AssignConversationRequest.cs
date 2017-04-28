using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskPam.Models.Conversations
{
    public class AssignConversationRequest
    {
        public int conversationId { get; set; }
        public string userId { get; set; }
    }
}
