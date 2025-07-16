using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ADUSAPI.Context
{
    public class ADUSContextFactory : IDesignTimeDbContextFactory<ADUSContext>
    {
        public ADUSContext CreateDbContext(string[] args)
        {
            // Lê o appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ADUSContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ADUS"));

            return new ADUSContext(configuration);
        }
    }
}