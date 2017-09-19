using System.Threading.Tasks;
using Acr.UserDialogs;
using MemoryApi.HttpClient;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;

namespace MemoryClient.Cross.Core.ViewModels
{
    /// <summary>
    /// View model for initialization/loading screen.
    /// </summary>
    /// <seealso cref="AppViewModel" />
    public class LoadingScreenViewModel : AppViewModel
    {
        private readonly IAuthClient _authClient;
        private readonly IApplicationSettings _settings;
        private readonly IMvxNavigationService _navigation;

        /// <inheritdoc />
        public LoadingScreenViewModel(IAuthClient authClient, IApplicationSettings settings, IMvxNavigationService navigation)
        {
            _authClient = authClient;
            _settings = settings;
            _navigation = navigation;
        }

        /// <inheritdoc />
        public override async void Start()
        {
            var authToken = _settings.AuthToken;
            if (string.IsNullOrEmpty(authToken))
            {
                await _navigation.Navigate(typeof(LoginDialogViewModel));
                return;
            }

            try
            {
                var validateResult = await _authClient.ValidateAsync(authToken);
                //await _navigation.Navigate<Dashboard>(new { UserId = validateResult });
            }
            catch (SwaggerException<string> e)
            {
                _settings.AuthToken = null;
                var localization = Mvx.Resolve<IMvxTextProvider>();
                await Mvx.Resolve<IUserDialogs>().AlertAsync($"{e.Data}\n\n{localization.GetText("", "LoadingScreen", "ExceptionText")}", localization.GetText("", "LoadingScreen", "ExceptionTitle"));
                Start();
            }
        }
    }
}