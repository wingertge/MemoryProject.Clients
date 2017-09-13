namespace MemoryClient.Cross.Core
{
    public interface IApplicationSettings
    {
        string AuthToken { get; set; }
        bool StayLoggedIn { get; set; }

        void CleanUp();
    }
}