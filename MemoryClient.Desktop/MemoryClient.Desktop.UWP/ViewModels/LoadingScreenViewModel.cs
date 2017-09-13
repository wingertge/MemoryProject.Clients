using System;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using MemoryApi.HttpClient;
using MemoryClient.Core.Resources;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace MemoryClient.Desktop.UWP.ViewModels
{
    public class LoadingScreenViewModel : MvxViewModel
    {
        private readonly IAuthClient _authClient;
        private readonly ApplicationSettings _settings;
        private readonly IMvxNavigationService _navigation;

        public LoadingScreenViewModel(IAuthClient authClient, ApplicationSettings settings, IMvxNavigationService navigation)
        {
            _authClient = authClient;
            _settings = settings;
            _navigation = navigation;
        }

        public override async void Start()
        {
            var authToken = _settings.AuthToken;
            if (string.IsNullOrEmpty(authToken))
            {
                //ShowViewModel<LoginDialogViewModel>();
            }

            try
            {
                var validateResult = await _authClient.ValidateAsync(authToken);
                //ShowViewModel<Dashboard>(new { UserId = validateResult });
            }
            catch (SwaggerException<string> e)
            {
                var resources = new ResourceLoader();
                _settings.AuthToken = null;
                var errorDialog = new ContentDialog
                {
                    Title = AppResources.AuthErrorTitle,
                    Content = $"{e.Result}\n\n{resources.GetString("LoginAgainText")}",
                    CloseButtonText = resources.GetString("ConfirmError")
                };

                await errorDialog.ShowAsync();
                await _navigation.Navigate<LoadingScreenViewModel>();
            }
        }
    }
}