using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SakilaConsoleApp
{
    public class SakilaDbContextFactory : IDesignTimeDbContextFactory<SakilaDbContext>
    {
        public SakilaDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            var builder = new DbContextOptionsBuilder<SakilaDbContext>();
            builder.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 32)));
            return new SakilaDbContext(builder.Options);
        }
    }
}
