using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using MemoryApi.HttpClient;
using MemoryCore.JsonModels;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace MemoryClient.Cross.Core.ViewModels
{
    /// <summary>
    /// View model for Registration form
    /// </summary>
    /// <seealso cref="AppViewModel" />
    public class RegisterDialogViewModel : AppViewModel<RegisterDialogViewModel.NavProperties>
    {
        private bool IsProvider { get; set; }
        private LoginProvider Provider { get; set; }
        private string Token { get; set; }

        private string _email;

        /// <summary>
        /// Data representation of the email input
        /// </summary>
        /// <value>The email.</value>
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _username;

        /// <summary>
        /// Data representation of the username input
        /// </summary>
        /// <value>The username.</value>
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;

        /// <summary>
        /// Data representation of the password input
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _passwordAgain;

        /// <summary>
        /// Data representation of the password confirm input
        /// </summary>
        /// <value>The password again.</value>
        public string PasswordAgain
        {
            get => _passwordAgain;
            set => SetProperty(ref _passwordAgain, value);
        }

        private string _globalErrorMessage;

        /// <summary>
        /// Global error message to be displayed on login form
        /// </summary>
        /// <value>The global error message or null if no error is present.</value>
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
        /// Used for display logic of global error message
        /// </summary>
        /// <value><c>true</c> if global error should be shown, otherwise <c>false</c>.</value>
        public bool ShowGlobalError => !string.IsNullOrEmpty(GlobalErrorMessage);

        private readonly Lazy<MvxAsyncCommand> _registerCommand;

        /// <summary>
        /// Command to fire when register button is clicked
        /// </summary>
        /// <value>The register command.</value>
        public ICommand RegisterCommand => _registerCommand.Value;

        private readonly Lazy<MvxAsyncCommand> _cancelCommand;

        /// <summary>
        /// Command to fire when cancel/back button is clicked
        /// </summary>
        /// <value>The cancel command.</value>
        public ICommand CancelCommand => _cancelCommand.Value;

        /// <summary>
        /// Validator for the email input
        /// </summary>
        /// <seealso cref="ValidateEmail"/>
        public readonly Func<string, string> EmailValidator;

        /// <summary>
        /// Validator for the username input
        /// </summary>
        /// <seealso cref="ValidateUsername" />
        public readonly Func<string, string> UsernameValidator = ValidateUsername;

        /// <summary>
        /// Error validator for the password input
        /// </summary>
        /// <seealso cref="ValidatePasswordError"/>
        public readonly Func<string, string> PasswordErrorValidator = ValidatePasswordError;

        /// <summary>
        /// Warning validator for the password input. Warning is not blocking.
        /// </summary>
        /// <seealso cref="ValidatePasswordWarning"/>
        public readonly Func<string, string> PasswordWarningValidator = ValidatePasswordWarning;

        /// <summary>
        /// Validator for the password confirm input.
        /// </summary>
        public readonly Func<string, string> PasswordAgainValidator;

        private bool _emailValid;

        /// <summary>
        /// Property indicating whether the email is valid.
        /// </summary>
        /// <value><c>true</c> if email is valid, otherwise <c>false</c>.</value>
        public bool EmailValid
        {
            private get => _emailValid;
            set
            {
                if(_emailValid != value) RaisePropertyChanged(nameof(FormValid));
                SetProperty(ref _emailValid, value);
            }
        }

        private bool _usernameValid;

        /// <summary>
        /// Property indicating whether the username is valid.
        /// </summary>
        /// <value><c>true</c> if username is valid, otherwise <c>false</c>.</value>
        public bool UsernameValid
        {
            private get => _usernameValid;
            set
            {
                if(_usernameValid != value) RaisePropertyChanged(nameof(FormValid));
                SetProperty(ref _usernameValid, value);
            }
        }

        private bool _passwordValid;

        /// <summary>
        /// Property indicating whether the password is valid.
        /// </summary>
        /// <value><c>true</c> if password is valid, otherwise <c>false</c>.</value>
        public bool PasswordValid
        {
            private get => _passwordValid;
            set
            {
                if(_passwordValid != value) RaisePropertyChanged(nameof(FormValid));
                SetProperty(ref _passwordValid, value);
            }
        }

        private bool _passwordAgainValid;

        /// <summary>
        /// Property indicating whether the password confirmation is valid.
        /// </summary>
        /// <value><c>true</c> if password confirmation is valid, otherwise <c>false</c>.</value>
        public bool PasswordAgainValid
        {
            private get => _passwordAgainValid;
            set
            {
                if(_passwordAgainValid != value) RaisePropertyChanged(nameof(FormValid));
                SetProperty(ref _passwordAgainValid, value);
            }
        }

        /// <summary>
        /// Value indicating whether the form is valid.
        /// </summary>
        /// <value><c>true</c> if form is valid, otherwise <c>false</c>.</value>
        public bool FormValid => (IsProvider || EmailValid) && UsernameValid && PasswordValid && PasswordAgainValid;

        private readonly IMvxNavigationService _navigation;
        private readonly ValidationUtils _validation;
        private readonly IApplicationSettings _appSettings;

        /// <inheritdoc />
        public RegisterDialogViewModel(IMvxNavigationService navigation, ValidationUtils validation, IApplicationSettings appSettings)
        {
            _navigation = navigation;
            _validation = validation;
            _appSettings = appSettings;

            EmailValidator = val => IsProvider ? null : ValidateEmail(val);
            PasswordAgainValidator = val => Password == val ? null : "PasswordsDontMatch";
            _registerCommand = new Lazy<MvxAsyncCommand>(() => new MvxAsyncCommand(AttemptRegister));
            _cancelCommand = new Lazy<MvxAsyncCommand>(() => new MvxAsyncCommand(Close));
        }

        private string ValidateEmail(string value)
        {
            var valid = !string.IsNullOrEmpty(value) && _validation.IsValidEmail(value);
            return valid ? null : "EmailInvalidFormat";
        }

        private static string ValidateUsername(string value)
        {
            if (value.Length < 3 || value.Length > 15) return "UsernameInvalidLength";
            return Regex.IsMatch(value, @"^[a-zA-Z][a-zA-Z0-9_-]+$") ? null : "UsernameInvalidFormat";
        }

        private static string ValidatePasswordError(string value) => Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$") ? null : "PasswordInvalidFormat";
        private static string ValidatePasswordWarning(string value) => Regex.IsMatch(value, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$") ? null : "PasswordWeak";

        private async Task AttemptRegister()
        {
            var userClient = Mvx.Resolve<IUsersClient>();
            try
            {
                if (IsProvider)
                {
                    _appSettings.AuthToken = await AttemptProviderRegister(userClient, Provider, Token, Username, Password);
                    _appSettings.LastProviderToken = Token;
                }
                else _appSettings.AuthToken = await AttemptRegularRegister(userClient, Email, Username, Password);
            }
            catch (SwaggerException<string> e)
            {
                GlobalErrorMessage = e.Result;
            }
        }

        /// <summary>
        /// Asynchronously attempts registration with a linked OpenID connect account.
        /// </summary>
        /// <param name="userClient">Users API client.</param>
        /// <param name="provider">The OpenID provider.</param>
        /// <param name="token">The provider access token.</param>
        /// <param name="username">The account username.</param>
        /// <param name="password">The account password.</param>
        /// <returns>The session authentication token.</returns>
        /// <exception cref="SwaggerException{TResult}">Registration failed. Error message in <see cref="SwaggerException{TResult}.Result" />.</exception>
        public static Task<string> AttemptProviderRegister(IUsersClient userClient, LoginProvider provider, string token, string username, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously attemps registration with an account email.
        /// </summary>
        /// <param name="userClient">The API client.</param>
        /// <param name="email">The account email.</param>
        /// <param name="username">The account username.</param>
        /// <param name="password">The account password.</param>
        /// <returns>The sesssion authentication token.</returns>
        /// <exception cref="SwaggerException{TResult}">Registration failed. Error message in <see cref="SwaggerException{TResult}" />.</exception>
        public static async Task<string> AttemptRegularRegister(IUsersClient userClient, string email, string username, string password)
        {
            var model = new RegisterModel
            {
                Email = email,
                Username = username,
                Password = password,
                PasswordConfirm = password //already validated match
            };

            return await userClient.RegisterAsync(model);
        }

        private async Task Close()
        {
            await _navigation.Close(this);
        }

        /// <summary>
        /// Navigation properties for this view model.
        /// </summary>
        public class NavProperties
        {
            /// <summary>
            /// Indicates wether to use provider registration or not
            /// </summary>
            /// <value><c>true</c> if using provider, otherwise <c>false</c>.</value>
            public bool IsProvider { get; set; }

            /// <summary>
            /// Carries the provider to use if <see cref="IsProvider"/> is true, else default.
            /// </summary>
            /// <value>The provider.</value>
            public LoginProvider Provider { get; set; }

            /// <summary>
            /// Carries the provider token to use if <see cref="IsProvider"/> is true, else null.
            /// </summary>
            /// <value>The identifier token.</value>
            public string Token { get; set; }
        }

        /// <summary>
        /// Prepares instance from navigation properties.
        /// </summary>
        /// <param name="parameter">The navigation properties.</param>
        /// <seealso cref="NavProperties" />
        public override void Prepare(NavProperties parameter)
        {
            IsProvider = parameter.IsProvider;
            Provider = parameter.Provider;
            Token = parameter.Token;
        }
    }
}