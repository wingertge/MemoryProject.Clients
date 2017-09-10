using MemoryClient.Xamarin.Infrastructure;
using Prism.Navigation;

namespace MemoryClient.Xamarin.ViewModels
{
    public class LoginViewModel : AppMapViewModelBase
    {


        public LoginViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
