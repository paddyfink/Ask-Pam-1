using AskPam.Helpers;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace AskPam.ViewModels
{
    public class BaseViewModel : ObservableObject
    {        
        public BaseViewModel()
        {
            CrossConnectivity.Current.ConnectivityChanged += (sender, e) =>
            {
                StatusMessage = (!e.IsConnected) 
                    ? Resources.AppResources.NoNetworkConnectionMessage 
                    : string.Empty;
            };
            
        }
        
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set { SetProperty(ref _statusMessage, value); }
        }
        
        public INavigation Navigation { get; set; }
       
        
    }
}

