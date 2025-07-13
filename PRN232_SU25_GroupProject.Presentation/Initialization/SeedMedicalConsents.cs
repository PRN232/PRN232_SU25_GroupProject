using System;
using System.Linq;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class ConsentSeeder
    {
        public static void SeedMedicalConsents(SchoolMedicalDbContext context)
        {
            if (context.MedicalConsents.Any()) return;

            var vaccineCampaign = context.VaccinationCampaigns.FirstOrDefault(c => c.CampaignName == "Tiêm cúm mùa 2025");
            var checkupCampaign = context.HealthCheckupCampaigns.FirstOrDefault(c => c.CampaignName == "Khám sức khỏe Đầu Năm 2025-2026");
            var students = context.Students.Where(s => new[] { "STU001", "STU002", "STU003", "STU004" }.Contains(s.StudentCode)).ToList();
            var parents = context.Parents.ToDictionary(p => p.Id);

            if (vaccineCampaign == null || checkupCampaign == null || !students.Any() || !parents.Any()) return;

            context.MedicalConsents.AddRange(
                // Vaccine consents
                new MedicalConsent
                {
                    ConsentType = ConsentType.Vaccine,
                    CampaignId = vaccineCampaign.Id,
                    StudentId = students.First(s => s.StudentCode == "STU001").Id,
                    ParentId = students.First(s => s.StudentCode == "STU001").ParentId,
                    ConsentGiven = true,
                    ConsentDate = DateTime.Today,
                    ParentSignature = parents[students.First(s => s.StudentCode == "STU001").ParentId].FullName,
                    Note = "Đồng ý tiêm chủng cúm mùa"
                },
                new MedicalConsent
                {
                    ConsentType = ConsentType.Vaccine,
                    CampaignId = vaccineCampaign.Id,
                    StudentId = students.First(s => s.StudentCode == "STU002").Id,
                    ParentId = students.First(s => s.StudentCode == "STU002").ParentId,
                    ConsentGiven = true,
                    ConsentDate = DateTime.Today,
                    ParentSignature = parents[students.First(s => s.StudentCode == "STU002").ParentId].FullName,
                    Note = "Đồng ý tiêm chủng cúm mùa"
                },
                new MedicalConsent
                {
                    ConsentType = ConsentType.Vaccine,
                    CampaignId = vaccineCampaign.Id,
                    StudentId = students.First(s => s.StudentCode == "STU003").Id,
                    ParentId = students.First(s => s.StudentCode == "STU003").ParentId,
                    ConsentGiven = false,
                    ConsentDate = DateTime.Today,
                    ParentSignature = parents[students.First(s => s.StudentCode == "STU003").ParentId].FullName,
                    Note = "Không đồng ý do dị ứng vắc-xin"
                },
                new MedicalConsent
                {
                    ConsentType = ConsentType.Vaccine,
                    CampaignId = vaccineCampaign.Id,
                    StudentId = students.First(s => s.StudentCode == "STU004").Id,
                    ParentId = students.First(s => s.StudentCode == "STU004").ParentId,
                    ConsentGiven = true,
                    ConsentDate = DateTime.Today,
                    ParentSignature = parents[students.First(s => s.StudentCode == "STU004").ParentId].FullName,
                    Note = "Đồng ý tiêm chủng cúm mùa"
                },
                // Health checkup consents
                new MedicalConsent
                {
                    ConsentType = ConsentType.HealthCheckup,
                    CampaignId = checkupCampaign.Id,
                    StudentId = students.First(s => s.StudentCode == "STU001").Id,
                    ParentId = students.First(s => s.StudentCode == "STU001").ParentId,
                    ConsentGiven = true,
                    ConsentDate = DateTime.Today,
                    ParentSignature = parents[students.First(s => s.StudentCode == "STU001").ParentId].FullName,
                    Note = "Đồng ý khám sức khỏe định kỳ"
                },
                new MedicalConsent
                {
                    ConsentType = ConsentType.HealthCheckup,
                    CampaignId = checkupCampaign.Id,
                    StudentId = students.First(s => s.StudentCode == "STU002").Id,
                    ParentId = students.First(s => s.StudentCode == "STU002").ParentId,
                    ConsentGiven = false,
                    ConsentDate = DateTime.Today,
                    ParentSignature = parents[students.First(s => s.StudentCode == "STU002").ParentId].FullName,
                    Note = "Không đồng ý do lịch trình cá nhân"
                },
                new MedicalConsent
                {
                    ConsentType = ConsentType.HealthCheckup,
                    CampaignId = checkupCampaign.Id,
                    StudentId = students.First(s => s.StudentCode == "STU003").Id,
                    ParentId = students.First(s => s.StudentCode == "STU003").ParentId,
                    ConsentGiven = true,
                    ConsentDate = DateTime.Today,
                    ParentSignature = parents[students.First(s => s.StudentCode == "STU003").ParentId].FullName,
                    Note = "Đồng ý khám sức khỏe định kỳ"
                },
                new MedicalConsent
                {
                    ConsentType = ConsentType.HealthCheckup,
                    CampaignId = checkupCampaign.Id,
                    StudentId = students.First(s => s.StudentCode == "STU004").Id,
                    ParentId = students.First(s => s.StudentCode == "STU004").ParentId,
                    ConsentGiven = true,
                    ConsentDate = DateTime.Today,
                    ParentSignature = parents[students.First(s => s.StudentCode == "STU004").ParentId].FullName,
                    Note = "Đồng ý khám sức khỏe định kỳ"
                }
            );

            context.SaveChanges();
        }
    }
}