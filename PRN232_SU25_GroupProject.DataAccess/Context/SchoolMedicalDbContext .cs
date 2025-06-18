using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PRN232_SU25_GroupProject.DataAccess.Context
{
    public class SchoolMedicalDbContext : IdentityDbContext<User, Role, int>
    {
        public SchoolMedicalDbContext(DbContextOptions<SchoolMedicalDbContext> options) : base(options) { }

        // User Management
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<SchoolNurse> SchoolNurses { get; set; }

        // Medical Profiles
        public DbSet<MedicalProfile> MedicalProfiles { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<ChronicDisease> ChronicDiseases { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<VisionHearing> VisionHearings { get; set; }

        // Medical Incidents & Events
        public DbSet<MedicalIncident> MedicalIncidents { get; set; }
        public DbSet<MedicationGiven> MedicationsGiven { get; set; }

        // Medication Management
        public DbSet<Medication> Medications { get; set; }
        public DbSet<StudentMedication> StudentMedications { get; set; }

        // Vaccination Management
        public DbSet<VaccinationCampaign> VaccinationCampaigns { get; set; }
        public DbSet<VaccinationConsent> VaccinationConsents { get; set; }
        public DbSet<VaccinationRecord> VaccinationRecords { get; set; }

        // Health Checkup Management
        public DbSet<HealthCheckupCampaign> HealthCheckupCampaigns { get; set; }
        public DbSet<HealthCheckupResult> HealthCheckupResults { get; set; }

        // System
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");

            // User - Student relationship
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(s => s.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // User relationships
            modelBuilder.Entity<Parent>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Parent>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SchoolNurse>()
                .HasOne(n => n.User)
                .WithOne()
                .HasForeignKey<SchoolNurse>(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Medical Profile relationships
            modelBuilder.Entity<MedicalProfile>()
                .HasOne(mp => mp.Student)
                .WithOne(s => s.MedicalProfile)
                .HasForeignKey<MedicalProfile>(mp => mp.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Allergy>()
                .HasOne<MedicalProfile>()
                .WithMany(mp => mp.Allergies)
                .HasForeignKey(a => a.MedicalProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChronicDisease>()
                .HasOne<MedicalProfile>()
                .WithMany(mp => mp.ChronicDiseases)
                .HasForeignKey(cd => cd.MedicalProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VisionHearing>()
                .HasOne<MedicalProfile>()
                .WithOne(mp => mp.VisionHearing)
                .HasForeignKey<VisionHearing>(vh => vh.MedicalProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Medical Incident relationships
            modelBuilder.Entity<MedicalIncident>()
                .HasOne(mi => mi.Student)
                .WithMany()
                .HasForeignKey(mi => mi.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalIncident>()
                .HasOne(mi => mi.Nurse)
                .WithMany()
                .HasForeignKey(mi => mi.NurseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicationGiven>()
                .HasOne(mg => mg.MedicalIncident)
                .WithMany(mi => mi.MedicationsGiven)
                .HasForeignKey(mg => mg.IncidentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MedicationGiven>()
                .HasOne(mg => mg.Medication)
                .WithMany()
                .HasForeignKey(mg => mg.MedicationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student Medication relationships
            modelBuilder.Entity<StudentMedication>()
                .HasOne(sm => sm.Student)
                .WithMany()
                .HasForeignKey(sm => sm.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vaccination relationships
            modelBuilder.Entity<VaccinationConsent>()
                .HasOne<VaccinationCampaign>()
                .WithMany(vc => vc.Consents)
                .HasForeignKey(vc => vc.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VaccinationConsent>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(vc => vc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VaccinationRecord>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(vr => vr.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VaccinationRecord>()
                .HasOne<VaccinationCampaign>()
                .WithMany()
                .HasForeignKey(vr => vr.CampaignId)
                .OnDelete(DeleteBehavior.Restrict);

            // Health Checkup relationships
            modelBuilder.Entity<HealthCheckupResult>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(hcr => hcr.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HealthCheckupResult>()
                .HasOne<HealthCheckupCampaign>()
                .WithMany()
                .HasForeignKey(hcr => hcr.CampaignId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure decimal precision
            modelBuilder.Entity<HealthCheckupResult>()
                .Property(h => h.Height)
                .HasPrecision(5, 2);

            modelBuilder.Entity<HealthCheckupResult>()
                .Property(h => h.Weight)
                .HasPrecision(5, 2);

            // Configure string lengths
            modelBuilder.Entity<Student>()
                .Property(s => s.StudentCode)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.FullName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Parent>()
                .Property(p => p.PhoneNumber)
                .HasMaxLength(15);

            modelBuilder.Entity<Medication>()
                .Property(m => m.Name)
                .HasMaxLength(200)
                .IsRequired();

            // Configure unique constraints
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.StudentCode)
                .IsUnique();

            // Configure enum conversions
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<MedicalIncident>()
                .Property(mi => mi.Type)
                .HasConversion<string>();

            modelBuilder.Entity<MedicalIncident>()
                .Property(mi => mi.Severity)
                .HasConversion<string>();

            modelBuilder.Entity<VaccinationCampaign>()
                .Property(vc => vc.Status)
                .HasConversion<string>();

            modelBuilder.Entity<VaccinationRecord>()
                .Property(vr => vr.Result)
                .HasConversion<string>();

            modelBuilder.Entity<HealthCheckupCampaign>()
                .Property(hc => hc.Status)
                .HasConversion<string>();

            // Configure default values
            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<MedicalProfile>()
                .Property(mp => mp.LastUpdated)
                .HasDefaultValueSql("GETDATE()");

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed default medications
            modelBuilder.Entity<Medication>().HasData(
                new Medication { Id = 1, Name = "Paracetamol", Type = "Pain Relief", Description = "For fever and pain", StockQuantity = 100, ExpiryDate = DateTime.Now.AddYears(2), StorageInstructions = "Store in cool, dry place" },
                new Medication { Id = 2, Name = "Ibuprofen", Type = "Anti-inflammatory", Description = "For inflammation and pain", StockQuantity = 50, ExpiryDate = DateTime.Now.AddYears(2), StorageInstructions = "Store in cool, dry place" },
                new Medication { Id = 3, Name = "Antiseptic Solution", Type = "Topical", Description = "For wound cleaning", StockQuantity = 25, ExpiryDate = DateTime.Now.AddYears(1), StorageInstructions = "Store in cool, dry place" },
                new Medication { Id = 4, Name = "Bandages", Type = "First Aid", Description = "For wound dressing", StockQuantity = 200, ExpiryDate = DateTime.Now.AddYears(3), StorageInstructions = "Store in dry place" }
            );

            // Seed admin user role
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = 2, Name = "Manager", NormalizedName = "MANAGER" },
                new Role { Id = 3, Name = "SchoolNurse", NormalizedName = "SCHOOLNURSE" },
                new Role { Id = 4, Name = "Parent", NormalizedName = "PARENT" }
            );
        }
    }
}
