using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class DataSeeder
    {
        public static void SeedDatabase(SchoolMedicalDbContext context)
        {
            if (!context.Medications.Any())
            {
                context.Medications.AddRange(
                    new Medication { Name = "Paracetamol", Type = "Pain Relief", Description = "For fever and pain", StockQuantity = 100, ExpiryDate = DateTime.Now.AddYears(2), StorageInstructions = "Store in cool, dry place" },
                new Medication { Name = "Ibuprofen", Type = "Anti-inflammatory", Description = "For inflammation and pain", StockQuantity = 50, ExpiryDate = DateTime.Now.AddYears(2), StorageInstructions = "Store in cool, dry place" },
                new Medication { Name = "Antiseptic Solution", Type = "Topical", Description = "For wound cleaning", StockQuantity = 25, ExpiryDate = DateTime.Now.AddYears(1), StorageInstructions = "Store in cool, dry place" },
                new Medication { Name = "Bandages", Type = "First Aid", Description = "For wound dressing", StockQuantity = 200, ExpiryDate = DateTime.Now.AddYears(3), StorageInstructions = "Store in dry place" }
                );
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
                    new Role { Name = "Manager", NormalizedName = "MANAGER", ConcurrencyStamp = Guid.NewGuid().ToString() },
                    new Role { Name = "SchoolNurse", NormalizedName = "SCHOOLNURSE", ConcurrencyStamp = Guid.NewGuid().ToString() },
                    new Role { Name = "Parent", NormalizedName = "PARENT", ConcurrencyStamp = Guid.NewGuid().ToString() }
                );
            }

            context.SaveChanges();
        }
    }
}
