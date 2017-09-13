using System.Threading.Tasks;
using MemoryApi.HttpClient;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace MemoryClient.Cross.Core.ViewModels
{
    public class LoadingScreenViewModel : MvxViewModel
    {
        private readonly IAuthClient _authClient;
        private readonly IApplicationSettings _settings;
        private readonly IMvxNavigationService _navigation;

        public LoadingScreenViewModel(IAuthClient authClient, IApplicationSettings settings, IMvxNavigationService navigation)
        {
            _authClient = authClient;
            _settings = settings;
            _navigation = navigation;
        }

        public async Task ViewDidAppear()
        {
            var authToken = _settings.AuthToken;
            if (string.IsNullOrEmpty(authToken))
            {
                ShowViewModel<LoginDialogViewModel>();
            }

            try
            {
                var validateResult = await _authClient.ValidateAsync(authToken);
                //await _navigation.Navigate<Dashboard>(new { UserId = validateResult });
            }
            catch (SwaggerException<string> e)
            {
                _settings.AuthToken = null;
                //await _navigation.Navigate<ErrorModalViewModel>();
            }
        }
    }
}