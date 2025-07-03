using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
﻿using Microsoft.AspNetCore.Identity;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class DataSeeder
    {
        public static void SeedDatabase(SchoolMedicalDbContext context)
        {
            context.Database.EnsureCreated();

            RoleAndUserSeeder.SeedRoles(context);
            RoleAndUserSeeder.SeedUsers(context);
            ParentAndStudentSeeder.SeedParents(context);
            ParentAndStudentSeeder.SeedStudents(context);
            MedicalProfileSeeder.SeedMedicalProfiles(context);
            MedicalProfileSeeder.SeedAllergies(context);
            MedicalProfileSeeder.SeedChronicDiseases(context);
            MedicationSeeder.SeedMedications(context);
            MedicationSeeder.SeedStudentMedications(context);
            SchoolNurseSeeder.SeedSchoolNurses(context);
            IncidentAndTreatmentSeeder.SeedMedicalIncidents(context);
            IncidentAndTreatmentSeeder.SeedMedicationsGiven(context);
            VaccinationSeeder.SeedVaccinationCampaigns(context);
            ConsentSeeder.SeedMedicalConsents(context);
            VaccinationSeeder.SeedVaccinationRecords(context);
            HealthCheckupSeeder.SeedHealthCheckupCampaigns(context);
            HealthCheckupSeeder.SeedHealthCheckupResults(context);
            NotificationSeeder.SeedNotifications(context);
            MedicalHistoryAndVisionHearingSeeder.SeedMedicalHistories(context);
            MedicalHistoryAndVisionHearingSeeder.SeedVisionHearing(context);
        }

        public static async Task SeedPasswordsAsync(UserManager<User> userManager)
        {
            await PasswordSeeder.SeedPasswordsAsync(userManager);
        }
    }
}