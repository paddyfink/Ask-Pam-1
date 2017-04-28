
using AskPam.Models.Conversations;
using AskPam.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace AskPam.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        
        public ItemDetailPage()
        {
            InitializeComponent();
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            ConversationMessagesListView.PropertyChanged += ConversationMessagesListView_PropertyChanged;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SendMessageText" && string.IsNullOrEmpty(viewModel.SendMessageText))
            {
                ScrollToBottom();
            }
        }

        private void ConversationMessagesListView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ScrollToBottom();
        }
        
        private void ScrollToBottom()
        {
            if (ConversationMessagesListView.ItemsSource != null)
            {
                var messages = viewModel.CurrentConversation.Messages;
                var target = viewModel.CurrentConversation.Messages[viewModel.CurrentConversation.Messages.Count - 1];
                ConversationMessagesListView.ScrollTo(target, ScrollToPosition.Start, true);
            }
        }

        protected override void OnAppearing()
        {
            viewModel.InitiateChatAsync();
            viewModel.LoadConversationCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            viewModel.EndChat();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {

        }
    }
}
