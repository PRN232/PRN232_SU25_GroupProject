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
                TargetGrades = "Khối 1-9",
                Status = CheckupStatus.Planned
            });
            context.SaveChanges();
        }

        public static void SeedHealthCheckupResults(SchoolMedicalDbContext context)
        {
            if (context.HealthCheckupResults.Any()) return;

            var camp = context.HealthCheckupCampaigns.FirstOrDefault();
            var nurse = context.SchoolNurses.FirstOrDefault();
            var stu = context.Students.FirstOrDefault(s => s.StudentCode == "STU003");

            if (camp == null || nurse == null || stu == null) return;

            context.HealthCheckupResults.Add(new HealthCheckupResult
            {
                StudentId = stu.Id,
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
            });
            context.SaveChanges();
        }
    }
}