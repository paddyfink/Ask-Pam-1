using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskPam.Models.Conversations
{
    public class ConversationList 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnreadCount { get; set; }
        public int? ContactId { get; set; }
        public string ContactFullName { get; set; }
        public Message LastMessage { get; set; }
        public string AssignedToId { get; set; }
        public string AssignedToFirstName { get; set; }
        public string AssignedToLasntName { get; set; }
        public bool IsFlagged { get; set; }
        public DateTime Date => LastMessage.Date;

        public string AvatarColor { get; set; }
        public bool? IsActive { get; set; }
    }
}
