using System;
using System.Linq;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class VaccinationSeeder
    {
        public static void SeedVaccinationCampaigns(SchoolMedicalDbContext context)
        {
            if (context.VaccinationCampaigns.Any()) return;

            context.VaccinationCampaigns.Add(new VaccinationCampaign
            {
                CampaignName = "Tiêm cúm mùa 2025",
                VaccineType = "Cúm A/B",
                ScheduledDate = DateTime.Today.AddDays(10),
                TargetGrades = "Khối 1-6",
                Status = VaccinationStatus.Planned
            });
            context.SaveChanges();
        }

        public static void SeedVaccinationRecords(SchoolMedicalDbContext context)
        {
            if (context.VaccinationRecords.Any()) return;

            var consents = context.MedicalConsents.Take(2).ToList();
            var nurse = context.SchoolNurses.FirstOrDefault();
            if (!consents.Any() || nurse == null) return;

            foreach (var consent in consents)
            {
                context.VaccinationRecords.Add(new VaccinationRecord
                {
                    StudentId = consent.StudentId,
                    CampaignId = consent.CampaignId,
                    NurseId = nurse.Id,
                    VaccineType = "Cúm A/B",
                    BatchNumber = "FLU2025-001",
                    VaccinationDate = DateTime.Today.AddDays(-1),
                    SideEffects = "Đau cánh tay nhẹ",
                    Result = VaccinationResult.Completed,
                });
            }

            context.SaveChanges();
        }
    }
}