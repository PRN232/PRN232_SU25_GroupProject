using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.DataAccess.Context
{
#pragma warning disable CS1591
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


        // Medical Incidents & Events
        public DbSet<MedicalIncident> MedicalIncidents { get; set; }
        public DbSet<MedicationGiven> MedicationsGiven { get; set; }

        // Medication Management
        public DbSet<Medication> Medications { get; set; }
        public DbSet<StudentMedication> StudentMedications { get; set; }

        // Vaccination Management
        public DbSet<VaccinationCampaign> VaccinationCampaigns { get; set; }
        public DbSet<MedicalConsent> MedicalConsents { get; set; }
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

            modelBuilder.Entity<StudentMedication>()
                .Property(sm => sm.IsApproved)
                .HasConversion<string>();

            // Vaccination relationships
            modelBuilder.Entity<MedicalConsent>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.ConsentType)
                    .HasConversion<string>()
                    .IsRequired();

                entity.HasOne<Student>()
                    .WithMany()
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Parent>()
                    .WithMany()
                    .HasForeignKey(x => x.ParentId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Không thể set FK chính xác với Campaign nếu dùng 2 bảng khác nhau.
                // Có thể để CampaignId kiểu int, hoặc nullable cả hai trường nếu muốn (VaccinationCampaignId, HealthCheckupCampaignId).

                // Hoặc nếu muốn rất chặt, dùng Table Per Hierarchy (TPH) hoặc Table Per Type (TPT) cho campaign.
            });


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


        }


    }
#pragma warning restore CS1591
}
