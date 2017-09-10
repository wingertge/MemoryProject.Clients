using System;

namespace MemoryClient.Web.Functions
{
    public static class Config
    {
        public static Environment Environment { get; set; }
        public static string CdnHost { get; set; }
        public static string ApiHost { get; set; }

        static Config()
        {
            CdnHost = System.Environment.GetEnvironmentVariable("CDN_HOST") ?? "https://cdn.project-memory.org";
            ApiHost = System.Environment.GetEnvironmentVariable("API_HOST") ?? "https://api.project-memory.org";
            Environment =
                System.Environment.GetEnvironmentVariable("FUNCTION_ENV")
                    ?.Equals("Production", StringComparison.InvariantCultureIgnoreCase) ?? false
                    ? Environment.Production
                    : Environment.Development;
        }
    }

    public enum Environment
    {
        Development,
        Production
    }
}