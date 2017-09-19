namespace MemoryClient.Cross.Core
{
    public interface IApplicationSettings
    {
        string AuthToken { get; set; }
        bool StayLoggedIn { get; set; }
        string LastProviderToken { get; set; }
        string LastIdentifier { get; set; }

        void CleanUp();
    }
}