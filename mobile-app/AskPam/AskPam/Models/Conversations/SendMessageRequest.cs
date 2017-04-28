using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskPam.Models.Conversations
{
    public class SendMessageRequest
    {
        public int ConversationId { get; set; }
        public string Text { get; set; }
        public bool IsNote { get; set; }
    }
}
