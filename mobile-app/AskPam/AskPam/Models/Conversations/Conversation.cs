
using AskPam.Models.Contacts;
using System.Collections.ObjectModel;

namespace AskPam.Models.Conversations
{
    public class Conversation: BaseDataObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SimpleContact Contact { get; set; }
        public string UserAssignedId { get; set; }
        public string UserAssignedFullName { get; set; }
        public string SmoochUserId { get; set; }
        public string Email { get; set; }
        public string AvatarColor { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsBotEnabled { get; set; }
    }
}
