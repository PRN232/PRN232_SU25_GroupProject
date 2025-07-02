using System.Linq;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class SchoolNurseSeeder
    {
        public static void SeedSchoolNurses(SchoolMedicalDbContext context)
        {
            if (context.SchoolNurses.Any()) return;

            foreach (var user in context.Users.Where(u => u.Role == UserRole.SchoolNurse))
            {
                context.SchoolNurses.Add(new SchoolNurse
                {
                    UserId = user.Id,
                    FullName = user.UserName == "nurse001" ? "Đặng Thị Yến" : "Phạm Quốc Tuấn",
                    Department = "Y tế học đường",
                    LicenseNumber = $"NUR-{user.Id:0000}"
                });
            }
            context.SaveChanges();
        }
    }
}