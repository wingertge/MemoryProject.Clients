using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace MemoryClient.Cross.Core.ViewModels
{
    public class RegisterViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigation;
        public RegisterViewModel(IMvxNavigationService navigation)
        {
            _navigation = navigation;
        }

        private async Task AttemptRegister()
        {
            //TODO: Implement
        }

        private async Task Close()
        {
            await _navigation.Close(this);
        }
    }
}