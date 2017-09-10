using MemoryClient.Xamarin.Infrastructure;
using Prism.Navigation;

namespace MemoryClient.Xamarin.ViewModels
{
    public class CreateEditLessonViewModel : AppMapViewModelBase
    {


        public CreateEditLessonViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
