using AskPam.Common;
using AskPam.Interfaces;
using AskPam.Models.Authorization;
using AskPam.Resources;
using AskPam.Views;
using Microsoft.Azure.Mobile.Analytics;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace AskPam.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        protected IAuthenticationService AuthorizationService => DependencyService.Get<IAuthenticationService>();
        protected IAccountStore AccountStore => DependencyService.Get<IAccountStore>();
        protected INotificationHub NotificationHub => DependencyService.Get<INotificationHub>();

        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;
            LoginCommand = new Command(async () => await LoginAsync(), () => LoginCanExecute());
            LogoutCommand = new Command(async() => await LogoutAsync());            
        }
                
        private bool LoginCanExecute()
        {
            return (CrossConnectivity.Current.IsConnected);
        }

        public ICommand LoginCommand { get; }

        public ICommand LogoutCommand { get; }


        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private async Task LoginAsync()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                AuthInfo result = await AuthorizationService.Login(new Login() { Email = this.Username, Password = this.Password });
                if (result != null)
                {
                    string organizationId = (result.OrganizationId != null) ? result.OrganizationId.ToString() : string.Empty;

                    AccountStore.CreateUserAccount(this.Username);
                    AccountStore.SaveAccountProperty(new System.Collections.Generic.KeyValuePair<string, string>(AccountStoreKeys.UsernameKey, this.Username));
                    AccountStore.SaveAccountProperty(new System.Collections.Generic.KeyValuePair<string, string>(AccountStoreKeys.TokenKey, result.IdToken));
                    AccountStore.SaveAccountProperty(new System.Collections.Generic.KeyValuePair<string, string>(AccountStoreKeys.OrgIdKey, organizationId));

                    if (result.UserInfoDto != null)
                    {
                        AccountStore.SaveAccountProperty(new System.Collections.Generic.KeyValuePair<string, string>(AccountStoreKeys.UserIdKey, result.UserInfoDto.Id));
                    }

                    await Navigation.PushAsync(new ItemsPage());

                    //once authenticated, remove login page from navigation stack
                    Navigation.RemovePage(this.Navigation.NavigationStack.First());
                    NotificationHub.RegisterNotification(string.Empty);

                }

            }
            catch (Exception e)
            {
                Analytics.TrackEvent("LoginAsync Failed", new Dictionary<string, string> { { "Source", "Message" }, { e.Source, e.Message } });
            }
            finally
            {
                IsBusy = false;
            }
        }


        private async Task LogoutAsync()
        {
            try
            {
                AccountStore.DeleteAccount();

                await Navigation.PushAsync(new LoginPage() { Title = AppResources.AskPamTitle });

                //once authenticated, remove login page from navigation stack
                Navigation.RemovePage(this.Navigation.NavigationStack.First());
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("LogoutAsync Failed", new Dictionary<string, string> { { "Source", "Message" }, { e.Source, e.Message } });
            }
        }
        
    }
}
