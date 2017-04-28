using System.Collections.Generic;
using System.Linq;
using AskPam.Interfaces;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Dependency(typeof(AskPam.iOS.Providers.PlatformAccountStoreProvider))]
namespace AskPam.iOS.Providers
{
    public class PlatformAccountStoreProvider : IAccountStore
    {
        public readonly string AuthServiceId = "{UNIQUE ID}";

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
        }

        public void DeleteAccountProperty(string key)
        {
            var account = GetAccount();
            account.Properties.Remove(key);
        }

        public void DeleteAccount()
        {
            AccountStore store = AccountStore.Create();
            var account = store.FindAccountsForService(AuthServiceId).FirstOrDefault();
            if (account != null)
            {
                store.Delete(account, AuthServiceId);
            }
        }

        public Account GetAccount()
        {
            AccountStore store = AccountStore.Create();
            var account = store.FindAccountsForService(AuthServiceId).FirstOrDefault();
            if (account == null && !string.IsNullOrEmpty(_userName))
            {
                account = new Account(_userName);
                store.Save(account, AuthServiceId);
            }

            return account;
        }
    }
}