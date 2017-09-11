using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using MemoryClient.Core.Resources;
using Xamarin.Forms.Xaml;

namespace MemoryClient.Xamarin.Localization
{
    public class TranslateExtension : IMarkupExtension
    {
        private static readonly string ResourceId = typeof(AppResources).FullName;
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null) return null;
            var resourceManager = new ResourceManager(ResourceId, typeof(AppResources).GetTypeInfo().Assembly);
            return resourceManager.GetString(Text, CultureInfo.CurrentCulture);
        }
    }
}