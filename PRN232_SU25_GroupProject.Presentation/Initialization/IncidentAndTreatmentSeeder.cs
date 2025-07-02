using System;
using System.Linq;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class IncidentAndTreatmentSeeder
    {
        public static void SeedMedicalIncidents(SchoolMedicalDbContext context)
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

        public static void SeedMedicationsGiven(SchoolMedicalDbContext context)
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
    }
}