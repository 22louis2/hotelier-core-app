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
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"{@Directory.GetCurrentDirectory()}\\{jsonPath}").Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseNpgsql(configuration.GetConnectionString("DbConnectionString"));
            return new AppDbContext(builder.Options);
        }
    }
}
