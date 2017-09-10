using MemoryClient.Xamarin.Infrastructure;
using Prism.Navigation;

namespace MemoryClient.Xamarin.ViewModels
{
    public class LessonListViewModel : AppMapViewModelBase
    {


        public LessonListViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
