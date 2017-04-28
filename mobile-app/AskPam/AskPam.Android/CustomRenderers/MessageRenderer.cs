//
// MessageRenderer.cs: Draw custom chat bubble in Xamarin.Forms using custom platform renderer.
//
// Author:
//   Egor Bo
//   Github: https://github.com/EgorBo/CrossChat-Xamarin.Forms
//

using System.ComponentModel;
using System.Net;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;
using AskPam.Droid.CustomRenderers;
using AskPam.Models.Conversations;
using AskPam.Controls;

[assembly: ExportRenderer(typeof(MessageViewCell), typeof(MessageRenderer))]

namespace AskPam.Droid.CustomRenderers
{
    public class MessageRenderer : ViewCellRenderer
    {
        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context)
        {
            var inflatorservice = (LayoutInflater)Forms.Context.GetSystemService(Android.Content.Context.LayoutInflaterService);
            var dataContext = item.BindingContext as Message;
            
            if (dataContext != null)
            {                
                bool isNote = (dataContext.IsNote == null) ? false : (bool)dataContext.IsNote;
                bool isSender = (dataContext.Status == "Sent" && !isNote);
                var resourceId = isNote ? Resource.Layout.message_item_note :
                isSender ? Resource.Layout.message_item_owner : Resource.Layout.message_item_opponent;
                var noteAddedByText = (isNote) ? "Note added by " : string.Empty;

                var template = (LinearLayout)inflatorservice.Inflate(resourceId, null, false);
                template.FindViewById<TextView>(Resource.Id.nickname).Text = $"{noteAddedByText} {dataContext.Name}:";
                template.FindViewById<TextView>(Resource.Id.message).Text = dataContext.Text;
                return template;
            }

            return base.GetCellCore(item, convertView, parent, context);
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return imageBitmap;
        }


        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);
        }
    }
}