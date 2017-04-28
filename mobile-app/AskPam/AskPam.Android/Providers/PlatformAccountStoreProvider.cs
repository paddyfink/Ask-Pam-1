using System.Collections.Generic;
using System.Linq;
using AskPam.Interfaces;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(AskPam.Droid.Providers.PlatformAccountStoreProvider))]
namespace AskPam.Droid.Providers
{
    public class PlatformAccountStoreProvider : IAccountStore
    {
        public readonly string AuthServiceId = "{UNIQUE ID}";

        private AccountStore _accountStore;

        private string _userName;
        public string Username => _userName;
        
        public Dictionary<string, string> AccountProperties
        {
            get
            {
                var account = GetAccount();
                return account?.Properties;
            }
        }

        public void CreateUserAccount(string userName)
        {
            _userName = userName;
            GetAccount();
        }

        public void SaveAccountProperty(KeyValuePair<string, string> accountProperty)
        {
            var account = GetAccount();
            account.Properties.Add(accountProperty.Key, accountProperty.Value);
            _accountStore.Save(account, AuthServiceId);
        }

        public void DeleteAccountProperty(string key)
        {
            var account = GetAccount();
            account.Properties.Remove(key);
        }

        public void DeleteAccount()
        {
            _userName = string.Empty;
            var account = GetAccount();
            if (account != null)
            {
                _accountStore.Delete(account, AuthServiceId);
            }
        }

        public Account GetAccount()
        {
            if (_accountStore == null)
            {
                _accountStore = AccountStore.Create(Android.App.Application.Context);
            }

            var account = _accountStore.FindAccountsForService(AuthServiceId).FirstOrDefault();
            if (account == null && !string.IsNullOrEmpty(_userName))
            {
                account = new Account(_userName);
                _accountStore.Save(account, AuthServiceId);
            }

            return account;
        }
        
    }
}