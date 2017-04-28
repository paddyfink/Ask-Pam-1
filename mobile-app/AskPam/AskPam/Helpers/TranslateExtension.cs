using System;
using System.Globalization;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using AskPam.Interfaces;

namespace AskPam.Helpers
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci;
        const string ResourceId = "AskPam.Resources.AppResources";

        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            ResourceManager rm = new ResourceManager(ResourceId,
                                    typeof(TranslateExtension).GetTypeInfo().Assembly);

            var translation = rm.GetString(Text, ci);

            if (translation == null)
            {
                translation = Text;
            }

            return translation;
        }
    }
}
