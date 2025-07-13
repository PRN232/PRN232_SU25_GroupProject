using System;
using System.Linq;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class MedicalProfileSeeder
    {
        public static void SeedMedicalProfiles(SchoolMedicalDbContext context)
        {
            if (context.MedicalProfiles.Any()) return;

            foreach (var stu in context.Students)
            {
                context.MedicalProfiles.Add(new MedicalProfile { StudentId = stu.Id, LastUpdated = DateTime.Now });
            }
            context.SaveChanges();
        }

        public static void SeedAllergies(SchoolMedicalDbContext context)
        {
            if (context.Allergies.Any()) return;

            var profileMap = context.MedicalProfiles.ToDictionary(p => p.StudentId, p => p.Id);

            context.Allergies.AddRange(
                new Allergy { MedicalProfileId = profileMap.Values.ElementAt(0), AllergyName = "Hải sản", Severity = "Cao", Symptoms = "Nổi mề đay, khó thở", Treatment = "Mang EpiPen" },
                new Allergy { MedicalProfileId = profileMap.Values.ElementAt(2), AllergyName = "Đậu phộng", Severity = "TB", Symptoms = "Ngứa miệng", Treatment = "Thuốc kháng histamine" },
                new Allergy { MedicalProfileId = profileMap.Values.ElementAt(4), AllergyName = "Bụi mịn", Severity = "Nhẹ", Symptoms = "Hắt hơi", Treatment = "Đeo khẩu trang" }
            );
            context.SaveChanges();
        }

        public static void SeedChronicDiseases(SchoolMedicalDbContext context)
        {
            if (context.ChronicDiseases.Any()) return;

            var profileMap = context.MedicalProfiles.ToDictionary(p => p.StudentId, p => p.Id);

            context.ChronicDiseases.AddRange(
                new ChronicDisease { MedicalProfileId = profileMap.Values.ElementAt(1), DiseaseName = "Hen phế quản", Medication = "Ventolin", Instructions = "Xịt khi khó thở" },
                new ChronicDisease { MedicalProfileId = profileMap.Values.ElementAt(3), DiseaseName = "Viêm da cơ địa", Medication = "Thuốc mỡ corticoid", Instructions = "Bôi tối trước ngủ" }
            );
            context.SaveChanges();
        }
    }
}