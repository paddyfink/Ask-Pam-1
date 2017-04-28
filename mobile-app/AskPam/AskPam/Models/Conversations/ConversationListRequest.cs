using AskPam.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskPam.Models.Conversations
{
    public class ConversationListRequest : PagedResultRequest
    {
        /// <summary>
        /// All = 1, AssignedToMe = 2, Unassigned = 3,Flagged = 4,SavedContact = 5,UnsavedContact = 6,FollowedByMe = 7,Archived = 8,
        /// </summary>
        public int? FilterId { get; set; }
        public string Search { get; set; }
    }
}
