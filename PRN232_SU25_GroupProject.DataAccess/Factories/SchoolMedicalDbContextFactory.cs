using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PRN232_SU25_GroupProject.DataAccess.Context;
using System.IO;

namespace PRN232_SU25_GroupProject.DataAccess.Factories
{
    public class SchoolMedicalDbContextFactory : IDesignTimeDbContextFactory<SchoolMedicalDbContext>
    {
        public SchoolMedicalDbContext CreateDbContext(string[] args)
        {
            // Truy cập file appsettings.json tại project API
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../PRN232_SU25_GroupProject.Presentation"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SchoolMedicalDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new SchoolMedicalDbContext(optionsBuilder.Options);
        }
    }
}
