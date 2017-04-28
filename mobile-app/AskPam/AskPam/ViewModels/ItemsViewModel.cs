using System;
using System.Threading.Tasks;
using AskPam.Helpers;
using Xamarin.Forms;
using AskPam.Models.Conversations;
using AskPam.Interfaces;
using AskPam.Models.Common;
using System.Linq;
using System.Windows.Input;
using AskPam.Resources;
using Microsoft.Azure.Mobile.Analytics;
using System.Collections.Generic;

namespace AskPam.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        protected IConversationService ConversationService => DependencyService.Get<IConversationService>();

        public ObservableRangeCollection<ConversationList> Items { get; set; }
        public ObservableRangeCollection<EnumValue> Filters { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command EnableFilterCommand { get; set; }
        public Command LoadFiltersCommand { get; set; }
        private ICommand _refreshCommand, _loadMoreCommand = null;

        private string _search = "";
        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value); }
        }

        private EnumValue _selectedFilter;
        public EnumValue SelectedFilter
        {
            get { return _selectedFilter; }
            set { SetProperty(ref _selectedFilter, value); }
        }


        private int _selectedFilterIndex;
        public int SelectedFilterIndex
        {
            get { return _selectedFilterIndex; }
            set { SetProperty(ref _selectedFilterIndex, value); }
        }


        private bool _isSearchEnabled;
        public bool IsSearchEnabled
        {
            get { return _isSearchEnabled; }
            set { SetProperty(ref _isSearchEnabled, value); }
        }

        public ItemsViewModel()
        {
            Title = AppResources.AskPamTitle;
            Items = new ObservableRangeCollection<ConversationList>();
            Filters = new ObservableRangeCollection<EnumValue>();
            IsSearchEnabled = false;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            EnableFilterCommand = new Command(() => ExecuteEnableFilterCommand());

            this.PropertyChanged += ItemsViewModel_PropertyChanged;
        }

        private void ExecuteEnableFilterCommand()
        {
            IsSearchEnabled = !IsSearchEnabled;
        }

        private async void ItemsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedFilterIndex")
            {
                SelectedFilter = Filters[SelectedFilterIndex];
                await ExecuteLoadItemsCommand();
            }
        }

        public ICommand RefreshCommand
        {
            get { return _refreshCommand ?? new Command(async () => await ExecuteRefreshCommand(), () => CanExecuteRefreshCommand()); }
        }

        public bool CanExecuteRefreshCommand()
        {
            return !IsBusy;
        }

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = false;
            await ExecuteLoadItemsCommand();
        }

        public async Task SearchCommand()
        {
            await ExecuteLoadFiltersCommand();
        }

        public ICommand LoadMoreCommand
        {
            get { return _loadMoreCommand ?? new Command<ConversationList>(async (item) => await ExecuteLoadItemsCommand(item), CanExecuteLoadMoreCommand); }
        }

        public bool CanExecuteLoadMoreCommand(ConversationList item)
        {
            return !IsBusy && Items.Count != 0 && Items.Last().Id == item.Id;
        }

        public async Task InitializeFormAsync()
        {
            await ExecuteLoadFiltersCommand();
        }

        #region Private

        private async Task ExecuteLoadItemsCommand(ConversationList item = null)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (item == null)
                {
                    Items.Clear();
                }

                var pageResult = await ConversationService.GetConversationsList(new ConversationListRequest()
                {
                    FilterId = SelectedFilter == null ? 1 : SelectedFilter.Id,
                    MaxResultCount = 20,
                    Search = Search,
                    SkipCount = Items.Count
                });

                Items.AddRange(pageResult.Items);
            }
            catch (Exception ex)
            {
                Analytics.TrackEvent("ExecuteLoadItemsCommand Failed", new Dictionary<string, string> { { "Source", "Message" }, { ex.Source, ex.Message } });

                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteLoadFiltersCommand()
        {
            try
            {
                Filters.Clear();

                var filters = await ConversationService.GetFilters();
                SelectedFilter = filters.First();
                Filters.ReplaceRange(filters);
            }
            catch (Exception ex)
            {
                Analytics.TrackEvent("ExecuteLoadFiltersCommand Failed", new Dictionary<string, string> { { "Source", "Message" }, { ex.Source, ex.Message } });

                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

    }
}