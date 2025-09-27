namespace HerdSync
{
    public static class HostEnvironmentExtensions
    {
        public static bool IsSSMS(this IHostEnvironment hostEnvironment)
        {
            return hostEnvironment.IsEnvironment("SSMS");
        }

        public static bool IsOracle(this IHostEnvironment hostEnvironment)
        {
            return hostEnvironment.IsEnvironment("Oracle");
        }
    }
}
