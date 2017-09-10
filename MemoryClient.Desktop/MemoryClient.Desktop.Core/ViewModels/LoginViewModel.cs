using System;
using MemoryClient.Desktop.Infrastructure;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MemoryClient.Mobile.ViewModels
{
    public class LoginViewModel : AppMapViewModelBase
    {


        public LoginViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
