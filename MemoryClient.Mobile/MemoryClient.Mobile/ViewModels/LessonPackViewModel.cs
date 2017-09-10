using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using MemoryClient.Mobile.Infrastructure;

namespace MemoryClient.Mobile.ViewModels
{
    public class LessonPackViewModel : AppMapViewModelBase
    {


        public LessonPackViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
