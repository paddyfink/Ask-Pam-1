using AskPam.Interfaces;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(AskPam.Droid.Providers.LocaleProvider))]
namespace AskPam.Droid.Providers
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
            var androidLocale = Java.Util.Locale.Default;
            netLanguage = androidLocale.ToString().Replace("_", "-");

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