using AskPam.Interfaces;
using AskPam.Resources;
using AskPam.Views;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AskPam
{
    public partial class App : Application
    {
        protected IAccountStore AccountStore => DependencyService.Get<IAccountStore>();

        public App()
        {
            InitializeComponent();

            var account = AccountStore.GetAccount();
            bool isAuthenticated = (account != null);
            Current.MainPage = GetMainPage(isAuthenticated);
        }
        
        protected override void OnStart()
        {
            Microsoft.Azure.Mobile.MobileCenter.Start("android={MOBILE CENTER UNIQUE APP GUID};ios={MOBILE CENTER UNIQUE APP GUID};",
                   typeof(Microsoft.Azure.Mobile.Analytics.Analytics), typeof(Microsoft.Azure.Mobile.Crashes.Crashes));
        }

        private Page GetMainPage(bool isAuthenticated)
        {
            if (!isAuthenticated)
                return new NavigationPage(new LoginPage() { Title = AppResources.AskPamTitle });

            return new NavigationPage(new ItemsPage());
        }
    }
}
