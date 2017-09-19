using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace MemoryClient.Cross.Core
{
    public class MicrosoftSignInHandler : IProviderSignInHandler
    {
        private PublicClientApplication _client;
        private static readonly string[] Scopes = {"User.Read"};

        public MicrosoftSignInHandler()
        {
            _client = new PublicClientApplication(Secrets.MicrosoftClientId);
        }

        public async Task<string> GetAccessToken(UIParent parent)
        {
            try
            {
                var authResult = await _client.AcquireTokenAsync(Scopes, parent);
                return authResult.AccessToken;
            }
            catch (MsalException e)
            {
                throw new ProviderAuthenticationException("TokenAcquireError", e);
            }
        }
    }

    public class ProviderAuthenticationException : Exception
    {
        public ProviderAuthenticationException(string errorMessage, Exception exception) : base(errorMessage, exception) { }
    }
}