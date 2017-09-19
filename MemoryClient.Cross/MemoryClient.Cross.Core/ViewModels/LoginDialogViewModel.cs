using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using MemoryApi.HttpClient;
using MemoryCore.JsonModels;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace MemoryClient.Cross.Core.ViewModels
{
    /// <summary>
    /// View model for login form
    /// </summary>
    /// <seealso cref="AppViewModel" />
    public class LoginDialogViewModel : AppViewModel
    {
        private readonly IMvxNavigationService _navigation;
        private readonly IApplicationSettings _appSettings;
        private readonly IAuthClient _authClient;

        private string _identifier;

        /// <summary>
        /// Data representation of email/username input
        /// </summary>
        /// <value>The email/username.</value>
        public string Identifier
        {
            get => _identifier;
            set => SetProperty(ref _identifier, value);
        }

        private string _password;

        /// <summary>
        /// Data representation of password input
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _stayLoggedIn;

        /// <summary>
        /// Data representation of checkbox indicating wether to save auth token
        /// </summary>
        /// <value><c>true</c> if should stay logged in, otherwise <c>false</c>.</value>
        public bool StayLoggedIn
        {
            get => _stayLoggedIn;
            set => SetProperty(ref _stayLoggedIn, value);
        }

        private string _globalErrorMessage;

        /// <summary>
        /// Data representation of the global error message shown in the form
        /// </summary>
        /// <value>The global error message.</value>
        public string GlobalErrorMessage
        {
            get => _globalErrorMessage;
            set
            {
                if(_globalErrorMessage != value) RaisePropertyChanged(nameof(ShowGlobalError));
                SetProperty(ref _globalErrorMessage, value);
            }
        }

        /// <summary>
        /// View logic determining wether to show error box or not
        /// </summary>
        /// <value><c>true</c> if [show global error]; otherwise, <c>false</c>.</value>
        public bool ShowGlobalError => !string.IsNullOrEmpty(_globalErrorMessage);

        private bool _isBusy;

        /// <summary>
        /// Value indicating wether the form is busy on an asynchrous task
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private readonly Lazy<MvxAsyncCommand> _loginCommand;

        /// <summary>
        /// Command bound to login button. <seealso cref="AttemptRegularLogin"/>
        /// </summary>
        /// <value>The login command.</value>
        public ICommand LoginCommand => _loginCommand.Value;

        private readonly Lazy<MvxAsyncCommand> _registerCommand;

        /// <summary>
        /// Command bound to register button/link. <seealso cref="NavigateRegister"/>
        /// </summary>
        /// <value>The register command.</value>
        public ICommand RegisterCommand => _registerCommand.Value;

        private readonly Lazy<MvxAsyncCommand> _googleCommand;

        /// <summary>
        /// Command for Google provider login button. <seealso cref="AttemptProviderLogin"/>
        /// </summary>
        /// <value>The Google login command.</value>
        public ICommand GoogleCommand => _googleCommand.Value;

        private readonly Lazy<MvxAsyncCommand> _facebookCommand;

        /// <summary>
        /// Command for Facebook provider login button. <seealso cref="AttemptProviderLogin"/>
        /// </summary>
        /// <value>The Facebook login command.</value>
        public ICommand FacebookCommand => _facebookCommand.Value;

        private readonly Lazy<MvxAsyncCommand> _microsoftCommand;

        /// <summary>
        /// Command for Microsoft provider login button. <seealso cref="AttemptProviderLogin"/>
        /// </summary>
        /// <value>The Microsoft login command.</value>
        public ICommand MicrosoftCommand => _microsoftCommand.Value;

        private readonly Lazy<MvxAsyncCommand> _twitterCommand;

        /// <summary>
        /// Command for Twitter provider login button. <seealso cref="AttemptProviderLogin"/>
        /// </summary>
        /// <value>The Twitter login command.</value>
        public ICommand TwitterCommand => _twitterCommand.Value;

        /// <inheritdoc />
        public LoginDialogViewModel(IMvxNavigationService navigation, IApplicationSettings appSettings, IAuthClient authClient)
        {
            _navigation = navigation;
            _appSettings = appSettings;
            _authClient = authClient;

            _twitterCommand = new Lazy<MvxAsyncCommand>(() => new MvxAsyncCommand(() => AttemptLogin(LoginProvider.Twitter)));
            _microsoftCommand = new Lazy<MvxAsyncCommand>(() => new MvxAsyncCommand(() => AttemptLogin(LoginProvider.Microsoft)));
            _facebookCommand = new Lazy<MvxAsyncCommand>(() => new MvxAsyncCommand(() => AttemptLogin(LoginProvider.Facebook)));
            _googleCommand = new Lazy<MvxAsyncCommand>(() => new MvxAsyncCommand(() => AttemptLogin(LoginProvider.Google)));
            _loginCommand = new Lazy<MvxAsyncCommand>(() => new MvxAsyncCommand(() => AttemptLogin(LoginProvider.MemoryProject)));
            _registerCommand = new Lazy<MvxAsyncCommand>(() => new MvxAsyncCommand(NavigateRegister));
        }

        /// <summary>
        /// Asynchronously navigates to the register form (most likely popup)
        /// </summary>
        /// <returns>void</returns>
        public async Task NavigateRegister()
        {
            await _navigation.Navigate(typeof(RegisterDialogViewModel), new RegisterDialogViewModel.NavProperties {IsProvider = false});
        }

        private async Task AttemptLogin(LoginProvider provider)
        {
            IsBusy = true;

            try
            {
                if (provider == LoginProvider.MemoryProject)
                {
                    _appSettings.AuthToken = await AttemptRegularLogin(_authClient, Identifier, Password);
                    _appSettings.StayLoggedIn = StayLoggedIn;
                    _appSettings.LastIdentifier = Identifier;
                }
                else
                {
                    var result = await AttemptProviderLogin(_authClient, _navigation, provider, _appSettings.LastProviderToken);
                    _appSettings.LastProviderToken = result.providerToken;
                    _appSettings.AuthToken = result.authToken;
                    _appSettings.StayLoggedIn = true;
                }
            }
            catch (SwaggerException<string> e)
            {
                Password = "";
                GlobalErrorMessage = e.Result;
            }

            IsBusy = false;
        }

        /// <summary>
        /// Attempts login with email/username and password
        /// </summary>
        /// <param name="apiClient">The API client.</param>
        /// <param name="identifier">The email/username.</param>
        /// <param name="password">The password.</param>
        /// <returns cref="Task{TResult}">The session authentication token.</returns>
        /// <exception cref="SwaggerException{TResult}">Login failed. See <see cref="SwaggerException{TResult}.Result"/> for error message.</exception>
        [ItemNotNull]
        public static async Task<string> AttemptRegularLogin([NotNull] IAuthClient apiClient, [NotNull] string identifier, [NotNull] string password)
        {
            var model = new LoginModel
            {
                Identifier = identifier,
                Password = password
            };

            return await apiClient.LoginAsync(model);
        }

        /// <summary>
        /// Attempts to login through a third party OpenID provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="token">The provider access token.</param>
        /// <returns cref="Task{TResult}">The Session authentication token.</returns>
        /// <exception cref="SwaggerException{TResult}">Login failed. See <see cref="SwaggerException{TResult}.Result"/> for error message.</exception>
        public static async Task<(string providerToken, string authToken)> AttemptProviderLogin([NotNull] IAuthClient apiClient, IMvxNavigationService nav, LoginProvider provider, [CanBeNull] string token)
        {
            var accessToken = token;

            if (string.IsNullOrEmpty(token))
            {
                IProviderSignInHandler handler;

                switch (provider)
                {
                    case LoginProvider.Google:
                        handler = new GoogleSignInHandler();
                        break;
                    case LoginProvider.Microsoft:
                        handler = new MicrosoftSignInHandler();
                        break;
                    case LoginProvider.Twitter:
                        handler = new TwitterSignInHandler();
                        break;
                    case LoginProvider.Facebook:
                        handler = new FacebookSignInHandler();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(provider), provider, null);
                }

                try
                {
                    accessToken = await handler.GetAccessToken(App.UIParent);
                }
                catch (ProviderAuthenticationException e)
                {
                    throw new SwaggerException<string>("", "", "", new Dictionary<string, IEnumerable<string>>(), e.Message, e);
                }
            }

            var model = new LoginProviderModel
            {
                Provider = provider,
                AccessToken = accessToken
            };

            try
            {
                var authToken = await apiClient.LoginProviderAsync(model);
                return (accessToken, authToken);
            }
            catch (SwaggerException<string> e)
            {
                if (int.Parse(e.StatusCode) == (int)HttpStatusCode.Unauthorized)
                {
                    await nav.Navigate(typeof(RegisterDialogViewModel),
                        new RegisterDialogViewModel.NavProperties
                        {
                            IsProvider = true,
                            Provider = provider,
                            Token = accessToken
                        });
                    return ("", "");
                }
                else throw;
            }
        }
    }
}