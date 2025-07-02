using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class RoleAndUserSeeder
    {
        public static void SeedRoles(SchoolMedicalDbContext context)
        {
            if (context.Roles.Any()) return;
            context.Roles.AddRange(
                new Role { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new Role { Name = "Manager", NormalizedName = "MANAGER", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new Role { Name = "SchoolNurse", NormalizedName = "SCHOOLNURSE", ConcurrencyStamp = Guid.NewGuid().ToString() },
                new Role { Name = "Parent", NormalizedName = "PARENT", ConcurrencyStamp = Guid.NewGuid().ToString() }
            );
            context.SaveChanges();
        }

        public static void SeedUsers(SchoolMedicalDbContext context)
        {
            if (context.Users.Any()) return;
            DateTime now = DateTime.Now;
            context.Users.AddRange(
                new User { UserName = "admin", NormalizedUserName = "ADMIN", Email = "admin@school.vn", NormalizedEmail = "ADMIN@SCHOOL.VN", Role = UserRole.Admin, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumber = "0911111000", PhoneNumberConfirmed = true },
                new User { UserName = "mgr001", NormalizedUserName = "MGR001", Email = "manager@school.vn", NormalizedEmail = "MANAGER@SCHOOL.VN", Role = UserRole.Manager, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumber = "0911111001", PhoneNumberConfirmed = true },
                new User { UserName = "nurse001", NormalizedUserName = "NURSE001", Email = "nurse1@school.vn", NormalizedEmail = "NURSE1@SCHOOL.VN", Role = UserRole.SchoolNurse, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumber = "0911111002", PhoneNumberConfirmed = true },
                new User { UserName = "nurse002", NormalizedUserName = "NURSE002", Email = "nurse2@school.vn", NormalizedEmail = "NURSE2@SCHOOL.VN", Role = UserRole.SchoolNurse, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumber = "0911111003", PhoneNumberConfirmed = true },
                new User { UserName = "parent001", NormalizedUserName = "PARENT001", Email = "p1@parent.vn", NormalizedEmail = "P1@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumber = "0909111001", PhoneNumberConfirmed = true },
                new User { UserName = "parent002", NormalizedUserName = "PARENT002", Email = "p2@parent.vn", NormalizedEmail = "P2@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumber = "0909111002", PhoneNumberConfirmed = true },
                new User { UserName = "parent003", NormalizedUserName = "PARENT003", Email = "p3@parent.vn", NormalizedEmail = "P3@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumber = "0909111003", PhoneNumberConfirmed = true },
                new User { UserName = "parent004", NormalizedUserName = "PARENT004", Email = "p4@parent.vn", NormalizedEmail = "P4@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumber = "0909111004", PhoneNumberConfirmed = true },
                new User { UserName = "parent005", NormalizedUserName = "PARENT005", Email = "p5@parent.vn", NormalizedEmail = "P5@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumber = "0909111005", PhoneNumberConfirmed = true },
                new User { UserName = "parent006", NormalizedUserName = "PARENT006", Email = "p6@parent.vn", NormalizedEmail = "P6@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumber = "0909111006", PhoneNumberConfirmed = true },
                new User { UserName = "parent007", NormalizedUserName = "PARENT007", Email = "p7@parent.vn", NormalizedEmail = "P7@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumber = "0909111007", PhoneNumberConfirmed = true }
            );
            context.SaveChanges();
        }
    }
}
