using DAL.Configuration.Database;

namespace DAL.Configuration
{
    public class HerdsyncDBContextFactory : IDesignTimeDbContextFactory<HerdsyncDBContext>
    {
        public HerdsyncDBContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "SSMS";

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connectionString = config.GetConnectionString("SSMSConnection");
            Console.WriteLine("Base path: " + AppContext.BaseDirectory);
            Console.WriteLine("Loaded connection string: " + connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<HerdsyncDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new HerdsyncDBContext(optionsBuilder.Options);
        }
    }
}