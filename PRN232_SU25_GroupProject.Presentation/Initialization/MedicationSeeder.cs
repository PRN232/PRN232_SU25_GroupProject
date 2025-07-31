using System;
using System.Linq;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class MedicationSeeder
    {
        public static void SeedMedications(SchoolMedicalDbContext context)
        {
            if (context.Medications.Any()) return;
            DateTime now = DateTime.Now;
            context.Medications.AddRange(
                new Medication { Name = "Paracetamol 500mg", Type = "PainRelief", Description = "Giảm đau, hạ sốt", StockQuantity = 300, ExpiryDate = now.AddYears(2), StorageInstructions = "Nơi khô ráo" },
                new Medication { Name = "Vitamin C 100mg", Type = "Supplement", Description = "Tăng đề kháng", StockQuantity = 400, ExpiryDate = now.AddYears(1), StorageInstructions = "Tránh ánh nắng" },
                new Medication { Name = "Amoxicillin 250mg", Type = "Antibiotic", Description = "Kháng sinh", StockQuantity = 200, ExpiryDate = now.AddYears(1), StorageInstructions = "Dưới 30°C" },
                new Medication { Name = "Salbutamol", Type = "Inhaler", Description = "Xịt hen", StockQuantity = 50, ExpiryDate = now.AddYears(1), StorageInstructions = "Lắc trước khi dùng" },
                new Medication { Name = "Ibuprofen 200mg", Type = "PainRelief", Description = "Giảm đau, chống viêm", StockQuantity = 180, ExpiryDate = now.AddYears(1), StorageInstructions = "Nơi khô ráo" },
                new Medication { Name = "Oral Rehydration Salts", Type = "Hydration", Description = "Bù nước", StockQuantity = 120, ExpiryDate = now.AddYears(1), StorageInstructions = "Bảo quản nơi mát" }
            );
            context.SaveChanges();
        }

        public static void SeedStudentMedications(SchoolMedicalDbContext context)
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
                    IsApproved = DataAccess.Enums.MedicationApprovalStatus.Approved,
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
                    IsApproved = DataAccess.Enums.MedicationApprovalStatus.Approved
                }
            );
            context.SaveChanges();
        }
    }
}