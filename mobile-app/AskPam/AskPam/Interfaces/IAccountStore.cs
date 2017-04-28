using System.Collections.Generic;
using Xamarin.Auth;

namespace AskPam.Interfaces
{
    public interface IAccountStore
    {
        string Username { get; }

        Dictionary<string, string> AccountProperties { get; }

        void CreateUserAccount(string userName);

        void DeleteAccount();

        void SaveAccountProperty(KeyValuePair<string, string> accountProperty);

        void DeleteAccountProperty(string key);

        Account GetAccount();
    }
}
