using AskPam.Models.Conversations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AskPam.Converters
{
    public class ConversationTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            if (value is Conversation conversation)
            {
                return conversation.Contact != null ? conversation.Contact.FullName : conversation.Name;
            }
            if (value is ConversationList conversationList)
            {
                return conversationList.ContactId.HasValue ? conversationList.ContactFullName : conversationList.Name;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
