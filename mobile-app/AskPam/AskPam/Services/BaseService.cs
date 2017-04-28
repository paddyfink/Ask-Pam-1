using AskPam.Common;
using AskPam.Interfaces;
using Xamarin.Auth;
using Xamarin.Forms;

namespace AskPam.Services
{
    public class BaseService
    {
        public BaseService()
        {
            LoadAccountStoreInfo();
        }

        protected IAccountStore AccountStore => DependencyService.Get<IAccountStore>();

        private Account _account;

        public string ServiceHost
        {
            get
            {
#if DEBUG
                return $"{ServiceUri.TestEnvironmentHost}";
#else
                return $"{ServiceUri.ReleaseEnvironmentHost}";
#endif
            }

        }


        public string RequestUri => $"{ServiceHost}/api/";
        
        public string OrgId { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }

        private void LoadAccountStoreInfo()
        {
            _account = AccountStore.GetAccount();
            if (_account != null)
            {
                Token = _account.Properties[AccountStoreKeys.TokenKey];
                OrgId = _account.Properties[AccountStoreKeys.OrgIdKey];
            }
        }
    }
}
