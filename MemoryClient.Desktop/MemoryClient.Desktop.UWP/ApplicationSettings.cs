using Windows.Storage;

namespace MemoryClient.Desktop.UWP
{
    public class ApplicationSettings
    {
        private readonly ApplicationDataContainer _localSettings;

        private string _authToken;
        private bool _stayLoggedIn;

        public string AuthToken
        {
            get => _authToken;
            set
            {
                _authToken = value;
                if(_stayLoggedIn) _localSettings.Values[nameof(AuthToken)] = value;
            }
        }

        public bool StayLoggedIn
        {
            get => _stayLoggedIn;
            set
            {
                _stayLoggedIn = value;
                _localSettings.Values[nameof(StayLoggedIn)] = value;
            }
        }

        public ApplicationSettings()
        {
            _localSettings = ApplicationData.Current.LocalSettings;

            _authToken = _localSettings.Values[nameof(AuthToken)] as string;
            _stayLoggedIn = _localSettings.Values[nameof(StayLoggedIn)] as bool? ?? false;
        }
    }
}