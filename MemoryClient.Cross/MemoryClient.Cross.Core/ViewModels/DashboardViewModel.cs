using System;
using System.Threading.Tasks;
using MemoryApi.HttpClient;
using MemoryCore.JsonModels;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace MemoryClient.Cross.Core.ViewModels
{
    public class DashboardViewModel : AppViewModel
    {
        private readonly IApplicationSettings _appSettings;
        private readonly IMvxNavigationService _navigation;
        private readonly IUsersClient _userClient;
        private User _user;

        private string _username;
        public string Username {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private MvxCommand _openActiveLessonsCmd;
        public MvxCommand OpenActiveLessonsCmd =>_openActiveLessonsCmd ?? (_openActiveLessonsCmd = new MvxCommand(OpenActiveLessons));
 
        private void OpenActiveLessons() => _navigation.Navigate(typeof(ActiveLessonsViewModel));

        private MvxCommand _openAccountSettingsCmd;
        public MvxCommand OpenAccountSettingsCmd =>_openAccountSettingsCmd ?? (_openAccountSettingsCmd = new MvxCommand(OpenAccountSettings));
 
        private void OpenAccountSettings() => _navigation.Navigate(typeof(AccountSettingsViewModel));

        private MvxCommand _logoutCmd;
        public MvxCommand LogoutCmd =>_logoutCmd ?? (_logoutCmd = new MvxCommand(Logout));
 
        private void Logout()
        {
            _appSettings.StayLoggedIn = false;
            _appSettings.AuthToken = null;

            _navigation.Navigate(typeof(LoadingScreenViewModel));
        }

        public DashboardViewModel(IApplicationSettings appSettings, IMvxNavigationService navigation, IUsersClient userClient)
        {
            _appSettings = appSettings;
            _navigation = navigation;
            _userClient = userClient;
        }

        public async Task Init()
        {
            _user = await _userClient.GetCurrentUserDataAsync(_appSettings.AuthToken);
            Username = _user.DisplayName;
        }
    }
}