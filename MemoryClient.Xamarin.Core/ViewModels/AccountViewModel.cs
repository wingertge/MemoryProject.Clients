using MemoryClient.Xamarin.Infrastructure;
using Prism.Navigation;

namespace MemoryClient.Xamarin.ViewModels
{
    public class AccountViewModel : AppMapViewModelBase
    {


        public AccountViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
