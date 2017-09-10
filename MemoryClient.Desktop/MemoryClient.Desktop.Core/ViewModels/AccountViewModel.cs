using MemoryClient.Desktop.Infrastructure;
using Prism.Navigation;

namespace MemoryClient.Desktop.ViewModels
{
    public class AccountViewModel : AppMapViewModelBase
    {


        public AccountViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
