using MemoryClient.Cross.Core;
using MemoryClient.Cross.WPF.Properties;

namespace MemoryClient.Cross.WPF
{
    public class ApplicationSettings : IApplicationSettings
    {
        private string _authToken;
        private bool _stayLoggedIn;
        private string _lastProviderToken;
        private string _lastIdentifier;

        public string AuthToken
        {
            get => _authToken;
            set
            {
                _authToken = value;
                if (_stayLoggedIn) Settings.Default.AuthToken = value;
            }
        }

        public bool StayLoggedIn
        {
            get => _stayLoggedIn;
            set
            {
                _stayLoggedIn = value;
                Settings.Default.StayLoggedIn = value;
            }
        }

        public string LastProviderToken
        {
            get => _lastProviderToken;
            set
            {
                _lastProviderToken = value;
                Settings.Default.LastProviderToken = value;
            }
        }

        public string LastIdentifier
        {
            get => _lastIdentifier;
            set
            {
                _lastIdentifier = value;
                Settings.Default.LastIdentifier = value;
            }
        }

        public void CleanUp()
        {
            Settings.Default.Save();
        }
    }
}