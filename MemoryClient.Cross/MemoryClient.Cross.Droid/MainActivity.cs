using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Microsoft.Identity.Client;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace MemoryClient.Cross.Droid
{
    [Activity(Label = "MainActivity", MainLauncher = true)]
    public class MainActivity : MvxAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Core.App.UIParent = new UIParent(this);
            SetContentView(Resource.Layout.SplashScreen);
            UserDialogs.Init(this);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}