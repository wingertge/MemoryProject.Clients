using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;

namespace MemoryClient.Cross.Core.ViewModels
{
    public class LoginDialogViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigation;

        private string _identifier;
        public string Identifier
        {
            get => _identifier;
            set => SetProperty(ref _identifier, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _stayLoggedIn;
        public bool StayLoggedIn
        {
            get => _stayLoggedIn;
            set => SetProperty(ref _stayLoggedIn, value);
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                _loginCommand = _loginCommand ?? new MvxAsyncCommand(AttemptLogin);
                return _loginCommand;
            }
        }

        private ICommand _registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                _registerCommand = _registerCommand ?? new MvxAsyncCommand(NavigateRegister);
                return _registerCommand;
            }
        }

        public LoginDialogViewModel(IMvxNavigationService navigation)
        {
            _navigation = navigation;
        }

        private async Task NavigateRegister()
        {
            await _navigation.Navigate<RegisterViewModel>();
        }

        private async Task AttemptLogin()
        {
            //TODO: Implement!
        }

        public void Init()
        {
        }
    }
}