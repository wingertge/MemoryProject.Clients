using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using MemoryClient.Mobile.Infrastructure;

namespace MemoryClient.Mobile.ViewModels
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
