﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class DataSeeder
    {
        public static void SeedDatabase(SchoolMedicalDbContext context)
        {
            context.Database.EnsureCreated();

            SeedRoles(context);                  // 1. Quyền
            SeedUsers(context);                  // 2. Tài khoản
            SeedParents(context);                // 3. Phụ huynh (dùng UserId)
            SeedStudents(context);               // 4. Học sinh  (dùng ParentId)

            SeedMedicalProfiles(context);        // 5. Hồ sơ y tế gốc
            SeedAllergies(context);              // 5a. Dị ứng
            SeedChronicDiseases(context);        // 5b. Bệnh mãn tính

            SeedMedications(context);            // 6. Kho thuốc
            SeedStudentMedications(context);     // 7. Thuốc phụ huynh gửi

            SeedSchoolNurses(context);           // 8. Bản ghi y tá
            SeedMedicalIncidents(context);       // 9. Sự cố + điều trị
            SeedMedicationsGiven(context);       // 9a. Thuốc đã cấp khi sự cố

            SeedVaccinationCampaigns(context);   // 10. Chiến dịch tiêm chủng
            SeedVaccinationConsents(context);    // 11. Phiếu đồng ý

            SeedHealthCheckupCampaigns(context); // 12. Chiến dịch khám định kỳ
            SeedHealthCheckupResults(context);   // 13. Kết quả khám

            SeedNotifications(context);          // 14. Thông báo hệ thống
        }

        /*──────────────────────────────  ROLE / USER  ──────────────────────────────*/
        private static void SeedRoles(SchoolMedicalDbContext context)
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

        private static void SeedUsers(SchoolMedicalDbContext context)
        {
            if (context.Users.Any()) return;

            DateTime now = DateTime.Now;
            context.Users.AddRange(
                new User { UserName = "admin", NormalizedUserName = "ADMIN", Email = "admin@school.vn", NormalizedEmail = "ADMIN@SCHOOL.VN", Role = UserRole.Admin, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumberConfirmed = true },
                new User { UserName = "mgr001", NormalizedUserName = "MGR001", Email = "manager@school.vn", NormalizedEmail = "MANAGER@SCHOOL.VN", Role = UserRole.Manager, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumberConfirmed = true },
                new User { UserName = "nurse001", NormalizedUserName = "NURSE001", Email = "nurse1@school.vn", NormalizedEmail = "NURSE1@SCHOOL.VN", Role = UserRole.SchoolNurse, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumberConfirmed = true },
                new User { UserName = "nurse002", NormalizedUserName = "NURSE002", Email = "nurse2@school.vn", NormalizedEmail = "NURSE2@SCHOOL.VN", Role = UserRole.SchoolNurse, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumberConfirmed = true },
                new User { UserName = "parent001", NormalizedUserName = "PARENT001", Email = "p1@parent.vn", NormalizedEmail = "P1@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumberConfirmed = true },
                new User { UserName = "parent002", NormalizedUserName = "PARENT002", Email = "p2@parent.vn", NormalizedEmail = "P2@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumberConfirmed = true },
                new User { UserName = "parent003", NormalizedUserName = "PARENT003", Email = "p3@parent.vn", NormalizedEmail = "P3@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumberConfirmed = true },
                new User { UserName = "parent004", NormalizedUserName = "PARENT004", Email = "p4@parent.vn", NormalizedEmail = "P4@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumberConfirmed = true },
                new User { UserName = "parent005", NormalizedUserName = "PARENT005", Email = "p5@parent.vn", NormalizedEmail = "P5@PARENT.VN", Role = UserRole.Parent, IsActive = true, CreatedAt = now, EmailConfirmed = true, PhoneNumberConfirmed = true }
            );
            context.SaveChanges();
        }

        /*──────────────────────────────  PARENT / STUDENT  ──────────────────────────────*/
        private static void SeedParents(SchoolMedicalDbContext context)
        {
            if (context.Parents.Any()) return;

            var mapping = new (string UserName, string FullName, string Phone, string Addr)[]
            {
                ("parent001","Nguyễn Thị Lan", "0909111001","12 Nguyễn Huệ, Q1"),
                ("parent002","Phạm Văn Long",  "0909111002","45 Pasteur, Q3"),
                ("parent003","Trần Thị Hòa",   "0909111003","67 Điện Biên Phủ, Q10"),
                ("parent004","Lê Văn Bình",    "0909111004","89 CMT8, Tân Bình"),
                ("parent005","Võ Thị Mai",     "0909111005","23A Hoàng Hoa Thám, Phú Nhuận")
            };

            foreach (var (userName, fullName, phone, addr) in mapping)
            {
                var user = context.Users.First(u => u.UserName == userName);
                context.Parents.Add(new Parent { UserId = user.Id, FullName = fullName, PhoneNumber = phone, Address = addr });
            }
            context.SaveChanges();
        }

        private static void SeedStudents(SchoolMedicalDbContext context)
        {
            if (context.Students.Any()) return;

            var parents = context.Parents.ToDictionary(p => p.FullName);

            context.Students.AddRange(
                new Student { StudentCode = "STU001", FullName = "Nguyễn Minh Khang", DateOfBirth = new DateTime(2012, 4, 3), Gender = "Nam", ClassName = "5A1", ParentId = parents["Nguyễn Thị Lan"].Id },
                new Student { StudentCode = "STU002", FullName = "Nguyễn Minh Khuê", DateOfBirth = new DateTime(2014, 7, 15), Gender = "Nữ", ClassName = "3B2", ParentId = parents["Nguyễn Thị Lan"].Id },
                new Student { StudentCode = "STU003", FullName = "Phạm Mai Chi", DateOfBirth = new DateTime(2013, 1, 20), Gender = "Nữ", ClassName = "4C1", ParentId = parents["Phạm Văn Long"].Id },
                new Student { StudentCode = "STU004", FullName = "Phạm Gia Huy", DateOfBirth = new DateTime(2011, 11, 2), Gender = "Nam", ClassName = "6A1", ParentId = parents["Phạm Văn Long"].Id },
                new Student { StudentCode = "STU005", FullName = "Trần Bảo Khánh", DateOfBirth = new DateTime(2015, 9, 5), Gender = "Nữ", ClassName = "2A4", ParentId = parents["Trần Thị Hòa"].Id },
                new Student { StudentCode = "STU006", FullName = "Lê Gia Bảo", DateOfBirth = new DateTime(2010, 12, 12), Gender = "Nam", ClassName = "7B3", ParentId = parents["Lê Văn Bình"].Id },
                new Student { StudentCode = "STU007", FullName = "Võ Hoàng Anh", DateOfBirth = new DateTime(2013, 5, 28), Gender = "Nam", ClassName = "4A3", ParentId = parents["Võ Thị Mai"].Id }
            );
            context.SaveChanges();
        }

        /*──────────────────────────────  MEDICAL PROFILES  ──────────────────────────────*/
        private static void SeedMedicalProfiles(SchoolMedicalDbContext context)
        {
            if (context.MedicalProfiles.Any()) return;

            foreach (var stu in context.Students)
            {
                context.MedicalProfiles.Add(new MedicalProfile { StudentId = stu.Id, LastUpdated = DateTime.Now });
            }
            context.SaveChanges();
        }

        private static void SeedAllergies(SchoolMedicalDbContext context)
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

        private static void SeedChronicDiseases(SchoolMedicalDbContext context)
        {
            if (context.ChronicDiseases.Any()) return;

            var profileMap = context.MedicalProfiles.ToDictionary(p => p.StudentId, p => p.Id);

            context.ChronicDiseases.AddRange(
                new ChronicDisease { MedicalProfileId = profileMap.Values.ElementAt(1), DiseaseName = "Hen phế quản", Medication = "Ventolin", Instructions = "Xịt khi khó thở" },
                new ChronicDisease { MedicalProfileId = profileMap.Values.ElementAt(3), DiseaseName = "Viêm da cơ địa", Medication = "Thuốc mỡ corticoid", Instructions = "Bôi tối trước ngủ" }
            );
            context.SaveChanges();
        }

        /*──────────────────────────────  MEDICATION STOCK  ──────────────────────────────*/
        private static void SeedMedications(SchoolMedicalDbContext context)
        {
            if (context.Medications.Any()) return;

            DateTime now = DateTime.Now;
            context.Medications.AddRange(
                new Medication { Name = "Paracetamol 500mg", Type = "PainRelief", Description = "Giảm đau hạ sốt", StockQuantity = 300, ExpiryDate = now.AddYears(2), StorageInstructions = "Nơi khô ráo" },
                new Medication { Name = "Vitamin C 100mg", Type = "Supplement", Description = "Tăng đề kháng", StockQuantity = 400, ExpiryDate = now.AddYears(1), StorageInstructions = "Tránh ánh nắng" },
                new Medication { Name = "Amoxicillin 250mg", Type = "Antibiotic", Description = "Kháng sinh", StockQuantity = 200, ExpiryDate = now.AddYears(1), StorageInstructions = "Dưới 30°C" },
                new Medication { Name = "Salbutamol", Type = "Inhaler", Description = "Xịt hen", StockQuantity = 50, ExpiryDate = now.AddYears(1), StorageInstructions = "Lắc trước khi dùng" }
            );
            context.SaveChanges();
        }

        private static void SeedStudentMedications(SchoolMedicalDbContext context)
        {
            if (context.StudentMedications.Any()) return;

            var stuDict = context.Students.ToDictionary(s => s.StudentCode);
            var parentDict = context.Parents.ToDictionary(p => p.Id);

            context.StudentMedications.AddRange(
                new StudentMedication
                {
                    StudentId = stuDict["STU002"].Id,
                    MedicationName = "Vitamin C 100mg",
                    Dosage = "1 viên",
                    Instructions = "Uống sau bữa sáng",
                    AdministrationTime = new TimeSpan(8, 0, 0),
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(30),
                    ParentId = stuDict["STU002"].ParentId,
                    IsApproved = true
                },
                new StudentMedication
                {
                    StudentId = stuDict["STU003"].Id,
                    MedicationName = "Salbutamol",
                    Dosage = "2 nhát xịt",
                    Instructions = "Khi lên cơn hen",
                    AdministrationTime = new TimeSpan(0, 0, 0),
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(6),
                    ParentId = stuDict["STU003"].ParentId,
                    IsApproved = true
                }
            );
            context.SaveChanges();
        }

        /*──────────────────────────────  SCHOOL NURSE  ──────────────────────────────*/
        private static void SeedSchoolNurses(SchoolMedicalDbContext context)
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

        /*──────────────────────────────  INCIDENTS & TREATMENT  ──────────────────────────────*/
        private static void SeedMedicalIncidents(SchoolMedicalDbContext context)
        {
            if (context.MedicalIncidents.Any()) return;

            var stu = context.Students.FirstOrDefault(s => s.StudentCode == "STU001");
            var nurse = context.SchoolNurses.FirstOrDefault();
            if (stu == null || nurse == null) return;

            context.MedicalIncidents.Add(new MedicalIncident
            {
                StudentId = stu.Id,
                NurseId = nurse.Id,
                Type = IncidentType.Fall,
                Description = "Ngã cầu thang",
                Symptoms = "Trầy xước đầu gối",
                Treatment = "Rửa vết thương, dán băng",
                Severity = IncidentSeverity.Low,
                ParentNotified = true,
                IncidentDate = DateTime.Today.AddDays(-1)
            });
            context.SaveChanges();
        }

        private static void SeedMedicationsGiven(SchoolMedicalDbContext context)
        {
            if (context.MedicationsGiven.Any()) return;

            var incident = context.MedicalIncidents.FirstOrDefault();
            var paracetamol = context.Medications.FirstOrDefault(m => m.Name.Contains("Paracetamol"));
            if (incident == null || paracetamol == null) return;

            context.MedicationsGiven.Add(new MedicationGiven
            {
                IncidentId = incident.Id,
                MedicationId = paracetamol.Id,
                Dosage = "500mg",
                GivenAt = incident.IncidentDate.AddMinutes(5)
            });
            context.SaveChanges();
        }

        /*──────────────────────────────  VACCINATION  ──────────────────────────────*/
        private static void SeedVaccinationCampaigns(SchoolMedicalDbContext context)
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

        private static void SeedVaccinationConsents(SchoolMedicalDbContext context)
        {
            if (context.VaccinationConsents.Any()) return;

            var campaign = context.VaccinationCampaigns.FirstOrDefault();
            var stu = context.Students.FirstOrDefault();
            if (campaign == null || stu == null) return;

            var parent = context.Parents.FirstOrDefault(p => p.Id == stu.ParentId);
            if (parent == null) return;

            context.VaccinationConsents.Add(new VaccinationConsent
            {
                CampaignId = campaign.Id,
                StudentId = stu.Id,
                ParentId = parent.Id,
                ConsentGiven = true,
                ConsentDate = DateTime.Today,
                ParentSignature = parent.FullName
            });
            context.SaveChanges();
        }

        /*──────────────────────────────  HEALTH CHECKUP  ──────────────────────────────*/
        private static void SeedHealthCheckupCampaigns(SchoolMedicalDbContext context)
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

        private static void SeedHealthCheckupResults(SchoolMedicalDbContext context)
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

        /*──────────────────────────────  NOTIFICATIONS  ──────────────────────────────*/
        private static void SeedNotifications(SchoolMedicalDbContext context)
        {
            if (context.Notifications.Any()) return;

            var admin = context.Users.FirstOrDefault(u => u.UserName == "admin");
            if (admin == null) return;

            context.Notifications.Add(new Notification
            {
                UserId = admin.Id,
                Title = "Khởi tạo dữ liệu mẫu thành công",
                Message = "Hệ thống đã sẵn sàng để đăng nhập và kiểm thử.",
                CreatedAt = DateTime.Now,
                IsRead = false,
                Type = "System"
            });
            context.SaveChanges();
        }
        public static async Task SeedPasswordsAsync(UserManager<User> userManager)
        {
            var defaultPassword = "String_1";

            var usernames = new[] { "admin", "mgr001", "nurse001", "nurse002", "parent001", "parent002", "parent003", "parent004", "parent005" };

            foreach (var username in usernames)
            {
                var user = await userManager.FindByNameAsync(username);
                if (user != null && string.IsNullOrEmpty(user.PasswordHash))
                {
                    var result = await userManager.AddPasswordAsync(user, defaultPassword);
                    if (!result.Succeeded)
                    {
                        Console.WriteLine($"⚠️ Failed to set password for {username}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }
    }


}
