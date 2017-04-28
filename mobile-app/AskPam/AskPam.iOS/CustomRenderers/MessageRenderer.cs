//
// MessageRenderer.cs: Draw custom chat bubble in Xamarin.Forms using custom platform renderer.
//
// Author:
//   Egor Bo
//   Github: https://github.com/EgorBo/CrossChat-Xamarin.Forms
//

using UIKit;
using Xamarin.Forms;
using AskPam.iOS.CustomRenderers;
using AskPam.Controls;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MessageViewCell), typeof(MessageRenderer))]

namespace AskPam.iOS.CustomRenderers
{
    public class MessageRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var dataContext = item.BindingContext as AskPam.Models.Conversations.Message;
            if (dataContext != null)
            {
                bool isNote = (dataContext.IsNote == null) ? false : (bool)dataContext.IsNote;
                bool isSender = (dataContext.Status == "Sent" && !isNote);
                
                var noteAddedByText = (isNote) ? "Note added by " : string.Empty;

                var cell = tv.DequeueReusableCell(!isSender ? BubbleCell.KeyLeft : BubbleCell.KeyRight) as BubbleCell;
                if (cell == null)
                    cell = new BubbleCell(dataContext, isSender);

                cell.Update(dataContext.Text);
            }

            return base.GetCell(item, reusableCell, tv);
        }
    }
}