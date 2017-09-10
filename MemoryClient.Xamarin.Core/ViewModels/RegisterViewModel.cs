using MemoryClient.Xamarin.Infrastructure;
using Prism.Navigation;

namespace MemoryClient.Xamarin.ViewModels
{
    public class RegisterViewModel : AppMapViewModelBase
    {


        public RegisterViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
