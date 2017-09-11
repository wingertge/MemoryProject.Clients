using Android.App;
using Android.Content.PM;
using Android.OS;
using Autofac;
using MemoryClient.Desktop;
using Prism.Autofac;

namespace MemoryClient.Mobile.Droid
{
    [Activity(Label = "MemoryClient.Mobile", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(ContainerBuilder container)
        {
            
        }
    }
}

