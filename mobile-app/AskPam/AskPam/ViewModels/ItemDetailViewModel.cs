using AskPam.Helpers;
using AskPam.Interfaces;
using AskPam.Models.Conversations;
using AskPam.Models.Users;
using AskPam.Services;
using Microsoft.Azure.Mobile.Analytics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AskPam.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private ChatService _chatService;

        protected IConversationService ConversationService => DependencyService.Get<IConversationService>();
        protected IUserService UserService => DependencyService.Get<IUserService>();

        public ConversationList Item { get; set; }
        public ObservableRangeCollection<UserList> Users { get; set; }

        private Conversation _currentConversation;
        public Conversation CurrentConversation
        {
            get { return _currentConversation; }
            set { SetProperty(ref _currentConversation, value); }
        }

        private string _sendMessageText;
        public string SendMessageText
        {
            get { return _sendMessageText; }
            set { SetProperty(ref _sendMessageText, value); }
        }
        
        private int _selectedUserIndex;
        public int SelectedUserIndex
        {
            get { return _selectedUserIndex; }
            set { SetProperty(ref _selectedUserIndex, value); }
        }
    
        private bool _isBotEnabled;
        public bool IsBotEnabled
        {
            get { return _isBotEnabled; }
            set { SetProperty(ref _isBotEnabled, value); }
        }

        public Command LoadConversationCommand { get; set; }
        public Command SendMessageCommand { get; set; }

        public ItemDetailViewModel(ConversationList item = null)
        {
            IsBusy = true;
            Title = item.ContactFullName;
            Item = item;
            Users = new ObservableRangeCollection<UserList>();
            LoadConversationCommand = new Command(async () => await LoadConversationAsync());
            SendMessageCommand = new Command(async () => await SendMessageAsync());

            this.PropertyChanged += ItemDetailViewModel_PropertyChanged;
        }

        
        private async void ItemDetailViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedUserIndex")
            {
                var selectedUser = Users[SelectedUserIndex];
                if (CurrentConversation.UserAssignedId != selectedUser.Id)
                {
                    await ConversationService.AssignToUser(
                        new AssignConversationRequest()
                        {
                            conversationId = CurrentConversation.Id,
                            userId = selectedUser.Id
                        }
                    );
                }
            }

            if (e.PropertyName == "IsBotEnabled")
            {
                if (CurrentConversation.IsBotEnabled.Value != IsBotEnabled)
                {
                    await ConversationService.EnableBot(CurrentConversation.Id);
                }
            }
        }

        public async void InitiateChatAsync()
        {
            _chatService = new ChatService();
            _chatService.OnMessageReceived += ChatService_OnMessageReceived;
            await _chatService.ConnectAsync();
        }

        public void EndChat()
        {
            _chatService.Disconnect();
            _chatService.OnMessageReceived -= ChatService_OnMessageReceived;
        }

        public async Task SendMessageAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(SendMessageText))
                {
                    SendMessageRequest messageRequest = new SendMessageRequest { ConversationId = Item.Id, Text = SendMessageText, IsNote = false };
                    var message = await ConversationService.SendMessage(messageRequest);
                    SendMessageText = "";
                }
            }
            catch (Exception ex)
            {
                Analytics.TrackEvent("SendMessageAsync Failed", new Dictionary<string, string> { { "Source", "Message" }, { ex.Source, ex.Message } });
            }
        }

        private async Task LoadConversationAsync()
        {
            try
            {
                IsBusy = true;
                Users.Clear();
                Users.ReplaceRange(await UserService.GetAllUsers());
                CurrentConversation = await ConversationService.GetConversation(Item.Id);
                if (CurrentConversation.UserAssignedId != null)
                {
                    SelectedUserIndex = Users.IndexOf(Users.First(u => u.Id == CurrentConversation.UserAssignedId));
                }

                //TMP, Currently the webserver is not ready to get IsBotEnabled
                if (CurrentConversation.IsBotEnabled.HasValue)
                {
                    IsBotEnabled = CurrentConversation.IsBotEnabled.Value;
                }
                else
                {
                    CurrentConversation.IsBotEnabled = false;
                    IsBotEnabled = false;
                }

                if (CurrentConversation.Messages == null)
                    CurrentConversation.Messages = new ObservableCollection<Message>();
            }
            catch(Exception ex)
            {
                Analytics.TrackEvent("LoadConversationAsync Failed", new Dictionary<string, string> { { "Source", "Message" }, { ex.Source, ex.Message } });
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ChatService_OnMessageReceived(object sender, ConversationList e)
        {
            Item = e;
            CurrentConversation.Messages.Add(e.LastMessage);
        }
        
    }
}