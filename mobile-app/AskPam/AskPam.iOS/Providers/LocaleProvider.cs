
using Foundation;
using Xamarin.Forms;
using AskPam.Interfaces;
using System.Globalization;
using System.Threading;

[assembly: Dependency(typeof(AskPam.iOS.Providers.LocaleProvider))]
namespace AskPam.iOS.Providers
{
    public class LocaleProvider : ILocalize
    {
        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                netLanguage = NSLocale.PreferredLanguages[0];
            }
            
            CultureInfo ci = null;
            try
            {
                ci = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException ex)
            {
                //culture not found, so fallback to English
                ci = new CultureInfo("en");                
            }

            return ci;
        }
    }
}