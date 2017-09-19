using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Microsoft.Identity.Client;

namespace MemoryClient.Cross.Core
{
    public class GoogleSignInHandler : IProviderSignInHandler
    {
        public async Task<string> GetAccessToken(UIParent parent)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                throw new ProviderAuthenticationException("ErrorAuthenticating", e);
            }
        }

        public class OfflineAccessGoogleAuthorizationCodeFlow : GoogleAuthorizationCodeFlow
        {
            public OfflineAccessGoogleAuthorizationCodeFlow(GoogleAuthorizationCodeFlow.Initializer initializer) : base(initializer) { }

            public override AuthorizationCodeRequestUrl CreateAuthorizationCodeRequest(string redirectUri)
            {
                return new GoogleAuthorizationCodeRequestUrl(new Uri(AuthorizationServerUrl))
                {
                    ClientId = ClientSecrets.ClientId,
                    Scope = string.Join(" ", Scopes),
                    RedirectUri = redirectUri,
                    AccessType = "offline",
                    ApprovalPrompt = "force"
                };
            }
        };
    }
}