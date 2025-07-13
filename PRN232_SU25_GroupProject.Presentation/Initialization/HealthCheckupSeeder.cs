using System;
using System.Linq;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class HealthCheckupSeeder
    {
        public static void SeedHealthCheckupCampaigns(SchoolMedicalDbContext context)
        {
            if (context.HealthCheckupCampaigns.Any()) return;

            context.HealthCheckupCampaigns.Add(new HealthCheckupCampaign
            {
                CampaignName = "Khám sức khỏe Đầu Năm 2025-2026",
                CheckupTypes = "Thị lực, Chiều cao, Cân nặng, Huyết áp",
                ScheduledDate = DateTime.Today.AddMonths(2),
                TargetGrades = "5A1, 3B2, 4C1, 6A1",
                Status = CheckupStatus.Planned
            });
            context.SaveChanges();
        }

        public static void SeedHealthCheckupResults(SchoolMedicalDbContext context)
        {
            if (context.HealthCheckupResults.Any()) return;

            var camp = context.HealthCheckupCampaigns.FirstOrDefault();
            var nurse = context.SchoolNurses.FirstOrDefault();
            var students = context.Students.Where(s => new[] { "STU003", "STU004" }.Contains(s.StudentCode)).ToList();

            if (camp == null || nurse == null || !students.Any()) return;

            context.HealthCheckupResults.AddRange(
                new HealthCheckupResult
                {
                    StudentId = students.First(s => s.StudentCode == "STU003").Id,
                    CampaignId = camp.Id,
                    Height = 132.4M,
                    Weight = 30.6M,
                    BloodPressure = "108/68",
                    VisionTest = "10/10",
                    HearingTest = "Bình thường",
                    GeneralHealth = "Tốt",
                    RequiresFollowup = false,
                    Recommendations = "Tiếp tục vận động thường xuyên",
                    CheckupDate = DateTime.Today,
                    NurseId = nurse.Id
                },
                new HealthCheckupResult
                {
                    StudentId = students.First(s => s.StudentCode == "STU004").Id,
                    CampaignId = camp.Id,
                    Height = 145.2M,
                    Weight = 38.5M,
                    BloodPressure = "110/70",
                    VisionTest = "9/10",
                    HearingTest = "Bình thường",
                    GeneralHealth = "Tốt",
                    RequiresFollowup = false,
                    Recommendations = "Duy trì chế độ ăn uống lành mạnh",
                    CheckupDate = DateTime.Today,
                    NurseId = nurse.Id
                }
            );
            context.SaveChanges();
        }
    }
}