using AskPam.Helpers;
using System;

namespace AskPam.Models.Conversations
{
    public class Message : ObservableObject
    {
        public int? Id { get; set; }
        public int ConversationId { get; set; }
        public string Text { get; set; }
        public bool Seen { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int AttachmentsCount { get; set; }

        public string Name { get; set; }
        public double? Sentiment { get; set; }

        public string Channel { get; set; }
        public string Type { get; set; }
        public bool? IsNote { get; set; }
        public string CreatedUserPicture { get; set; }
        public string CreatedUserFirstName { get; set; }
        public string CreatedUserLastName { get; set; }
        public string Avatar { get; set; }

        public string Picture
        {
            get
            {
                return string.IsNullOrEmpty(Avatar) ? CreatedUserPicture : Avatar;
            }
        }

    }
}
