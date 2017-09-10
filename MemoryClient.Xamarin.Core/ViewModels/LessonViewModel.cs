using System;
using MemoryClient.Xamarin.Infrastructure;
using Prism;
using Prism.Navigation;

namespace MemoryClient.Xamarin.ViewModels
{
    public class LessonViewModel : AppMapViewModelBase, IActiveAware
    {

#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }

        public LessonViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
