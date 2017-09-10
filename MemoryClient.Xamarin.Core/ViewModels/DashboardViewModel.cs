using MemoryClient.Xamarin.Infrastructure;
using Prism.Navigation;

namespace MemoryClient.Xamarin.ViewModels
{
    public class DashboardViewModel : AppMapViewModelBase
    {


        public DashboardViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
