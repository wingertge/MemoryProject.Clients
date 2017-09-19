using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace MemoryClient.Cross.Core
{
    public class FacebookSignInHandler : IProviderSignInHandler
    {
        public Task<string> GetAccessToken(UIParent parent)
        {
            throw new System.NotImplementedException();
        }
    }
}