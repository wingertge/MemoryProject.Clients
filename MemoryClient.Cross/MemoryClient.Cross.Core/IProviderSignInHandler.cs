using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace MemoryClient.Cross.Core
{
    public interface IProviderSignInHandler
    {
        Task<string> GetAccessToken(UIParent parent);
    }
}