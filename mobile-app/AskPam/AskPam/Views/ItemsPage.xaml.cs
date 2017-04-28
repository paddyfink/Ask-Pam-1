using AskPam.ViewModels;
using Xamarin.Forms;
using AskPam.Models.Conversations;

namespace AskPam.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();
            viewModel = new ItemsViewModel();
            BindingContext = viewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ConversationList;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.InitializeFormAsync();

            SearchEntry.TextChanged += SearchEntry_TextChanged;
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SearchEntry.TextChanged -= SearchEntry_TextChanged;
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if searchbar was cleared, be sure to reload all items
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                viewModel.LoadItemsCommand.Execute(null);
            }
        }

        private void Logout_Clicked(object sender, System.EventArgs e)
        {
            LoginViewModel vm = new LoginViewModel(this.Navigation);
            vm.LogoutCommand.Execute(null);
            vm = null;
        }

        private void Filter_Clicked(object sender, System.EventArgs e)
        {
            viewModel.SelectedFilterIndex =int.Parse(((MenuItem)sender).CommandParameter.ToString());
        }

        private void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            viewModel.EnableFilterCommand.Execute(null);
        }
    }
}
