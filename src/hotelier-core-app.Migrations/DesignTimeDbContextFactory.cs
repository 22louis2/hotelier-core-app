using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace hotelier_core_app.Migrations
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            string jsonPath = "appsettings.json";
            var basePath = Directory.GetCurrentDirectory();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(Path.Combine(basePath, jsonPath), optional: false, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("DbConnectionString");
            Console.WriteLine($"Loaded Connection String: {connectionString}");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("⚠️ Database connection string is null or empty! Check appsettings.json.");
            }

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql(connectionString);

            return new AppDbContext(builder.Options);
        }
    }


}
