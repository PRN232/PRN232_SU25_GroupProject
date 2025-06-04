using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Context
{
    public class SchoolHealthDbContext : DbContext
    {
        public SchoolHealthDbContext(DbContextOptions<SchoolHealthDbContext> options) : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentParent> StudentParents { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineRequest> MedicineRequests { get; set; }
        public DbSet<MedicalEvent> MedicalEvents { get; set; }
        public DbSet<MedicalEventMedicine> MedicalEventMedicines { get; set; }
        public DbSet<VaccinationCampaign> VaccinationCampaigns { get; set; }
        public DbSet<ConsentForm> ConsentForms { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<HealthCheckupCampaign> HealthCheckupCampaigns { get; set; }
        public DbSet<HealthCheckup> HealthCheckups { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names
            ConfigureTableNames(modelBuilder);

            // Configure relationships and constraints
            ConfigureRelationships(modelBuilder);

            // Configure unique constraints
            ConfigureUniqueConstraints(modelBuilder);

            // Configure indexes
            ConfigureIndexes(modelBuilder);

            // Configure enum conversions
            ConfigureEnumConversions(modelBuilder);

            // Configure default values
            ConfigureDefaultValues(modelBuilder);

            // Configure decimal precision
            ConfigureDecimalPrecision(modelBuilder);
        }

        private void ConfigureTableNames(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>().ToTable("School");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<StudentParent>().ToTable("StudentParent");
            modelBuilder.Entity<HealthRecord>().ToTable("HealthRecord");
            modelBuilder.Entity<Medicine>().ToTable("Medicine");
            modelBuilder.Entity<MedicineRequest>().ToTable("MedicineRequest");
            modelBuilder.Entity<MedicalEvent>().ToTable("MedicalEvent");
            modelBuilder.Entity<MedicalEventMedicine>().ToTable("MedicalEventMedicine");
            modelBuilder.Entity<VaccinationCampaign>().ToTable("VaccinationCampaign");
            modelBuilder.Entity<ConsentForm>().ToTable("ConsentForm");
            modelBuilder.Entity<Vaccination>().ToTable("Vaccination");
            modelBuilder.Entity<HealthCheckupCampaign>().ToTable("HealthCheckupCampaign");
            modelBuilder.Entity<HealthCheckup>().ToTable("HealthCheckup");
            modelBuilder.Entity<BlogPost>().ToTable("BlogPost");
            modelBuilder.Entity<Notification>().ToTable("Notification");
            modelBuilder.Entity<SystemLog>().ToTable("SystemLog");
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // User - School relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.School)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.SchoolID)
                .OnDelete(DeleteBehavior.SetNull);

            // Student - School relationship
            modelBuilder.Entity<Student>()
                .HasOne(s => s.School)
                .WithMany(sc => sc.Students)
                .HasForeignKey(s => s.SchoolID)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentParent relationships
            modelBuilder.Entity<StudentParent>()
                .HasOne(sp => sp.Student)
                .WithMany(s => s.StudentParents)
                .HasForeignKey(sp => sp.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentParent>()
                .HasOne(sp => sp.Parent)
                .WithMany(u => u.StudentParents)
                .HasForeignKey(sp => sp.ParentID)
                .OnDelete(DeleteBehavior.Restrict);

            // HealthRecord - Student relationship
            modelBuilder.Entity<HealthRecord>()
                .HasOne(hr => hr.Student)
                .WithMany(s => s.HealthRecords)
                .HasForeignKey(hr => hr.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HealthRecord>()
                .HasOne(hr => hr.UpdatedByUser)
                .WithMany(u => u.HealthRecords)
                .HasForeignKey(hr => hr.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull);

            // Medicine - School relationship
            modelBuilder.Entity<Medicine>()
                .HasOne(m => m.School)
                .WithMany(s => s.Medicines)
                .HasForeignKey(m => m.SchoolID)
                .OnDelete(DeleteBehavior.Restrict);

            // MedicineRequest relationships
            modelBuilder.Entity<MedicineRequest>()
                .HasOne(mr => mr.Student)
                .WithMany(s => s.MedicineRequests)
                .HasForeignKey(mr => mr.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MedicineRequest>()
                .HasOne(mr => mr.Parent)
                .WithMany(u => u.MedicineRequests)
                .HasForeignKey(mr => mr.ParentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicineRequest>()
                .HasOne(mr => mr.ApprovedByUser)
                .WithMany(u => u.ApprovedMedicineRequests)
                .HasForeignKey(mr => mr.ApprovedBy)
                .OnDelete(DeleteBehavior.SetNull);

            // MedicalEvent relationships
            modelBuilder.Entity<MedicalEvent>()
                .HasOne(me => me.Student)
                .WithMany(s => s.MedicalEvents)
                .HasForeignKey(me => me.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MedicalEvent>()
                .HasOne(me => me.HandledByUser)
                .WithMany(u => u.HandledMedicalEvents)
                .HasForeignKey(me => me.HandledBy)
                .OnDelete(DeleteBehavior.Restrict);

            // MedicalEventMedicine relationships
            modelBuilder.Entity<MedicalEventMedicine>()
                .HasOne(mem => mem.MedicalEvent)
                .WithMany(me => me.MedicalEventMedicines)
                .HasForeignKey(mem => mem.EventID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MedicalEventMedicine>()
                .HasOne(mem => mem.Medicine)
                .WithMany(m => m.MedicalEventMedicines)
                .HasForeignKey(mem => mem.MedicineID)
                .OnDelete(DeleteBehavior.SetNull);

            // VaccinationCampaign relationships
            modelBuilder.Entity<VaccinationCampaign>()
                .HasOne(vc => vc.School)
                .WithMany(s => s.VaccinationCampaigns)
                .HasForeignKey(vc => vc.SchoolID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VaccinationCampaign>()
                .HasOne(vc => vc.CreatedByUser)
                .WithMany(u => u.CreatedVaccinationCampaigns)
                .HasForeignKey(vc => vc.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // ConsentForm relationships
            modelBuilder.Entity<ConsentForm>()
                .HasOne(cf => cf.Student)
                .WithMany(s => s.ConsentForms)
                .HasForeignKey(cf => cf.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConsentForm>()
                .HasOne(cf => cf.Parent)
                .WithMany(u => u.ConsentForms)
                .HasForeignKey(cf => cf.ParentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Vaccination relationships
            modelBuilder.Entity<Vaccination>()
                .HasOne(v => v.Campaign)
                .WithMany(vc => vc.Vaccinations)
                .HasForeignKey(v => v.CampaignID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vaccination>()
                .HasOne(v => v.Student)
                .WithMany(s => s.Vaccinations)
                .HasForeignKey(v => v.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vaccination>()
                .HasOne(v => v.AdministeredByUser)
                .WithMany(u => u.AdministeredVaccinations)
                .HasForeignKey(v => v.AdministeredBy)
                .OnDelete(DeleteBehavior.Restrict);

            // HealthCheckupCampaign relationships
            modelBuilder.Entity<HealthCheckupCampaign>()
                .HasOne(hcc => hcc.School)
                .WithMany(s => s.HealthCheckupCampaigns)
                .HasForeignKey(hcc => hcc.SchoolID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HealthCheckupCampaign>()
                .HasOne(hcc => hcc.CreatedByUser)
                .WithMany(u => u.CreatedHealthCheckupCampaigns)
                .HasForeignKey(hcc => hcc.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // HealthCheckup relationships
            modelBuilder.Entity<HealthCheckup>()
                .HasOne(hc => hc.CheckupCampaign)
                .WithMany(hcc => hcc.HealthCheckups)
                .HasForeignKey(hc => hc.CheckupCampaignID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HealthCheckup>()
                .HasOne(hc => hc.Student)
                .WithMany(s => s.HealthCheckups)
                .HasForeignKey(hc => hc.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HealthCheckup>()
                .HasOne(hc => hc.Doctor)
                .WithMany(u => u.HealthCheckups)
                .HasForeignKey(hc => hc.DoctorID)
                .OnDelete(DeleteBehavior.SetNull);

            // BlogPost relationships
            modelBuilder.Entity<BlogPost>()
                .HasOne(bp => bp.Author)
                .WithMany(u => u.BlogPosts)
                .HasForeignKey(bp => bp.AuthorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BlogPost>()
                .HasOne(bp => bp.School)
                .WithMany(s => s.BlogPosts)
                .HasForeignKey(bp => bp.SchoolID)
                .OnDelete(DeleteBehavior.Restrict);

            // Notification - User relationship
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // SystemLog - User relationship
            modelBuilder.Entity<SystemLog>()
                .HasOne(sl => sl.User)
                .WithMany(u => u.SystemLogs)
            .HasForeignKey(sl => sl.UserID)
                .OnDelete(DeleteBehavior.SetNull);
        }

        private void ConfigureUniqueConstraints(ModelBuilder modelBuilder)
        {
            // User unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Student unique constraint
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.StudentCode)
                .IsUnique();

            // Medicine unique constraint
            modelBuilder.Entity<Medicine>()
                .HasIndex(m => m.MedicineCode)
                .IsUnique();

            // StudentParent unique constraint
            modelBuilder.Entity<StudentParent>()
                .HasIndex(sp => new { sp.StudentID, sp.ParentID })
                .IsUnique()
                .HasDatabaseName("UC_StudentParent");

            // Vaccination unique constraint
            modelBuilder.Entity<Vaccination>()
                .HasIndex(v => new { v.CampaignID, v.StudentID })
                .IsUnique()
                .HasDatabaseName("UC_StudentVaccine");

            // HealthCheckup unique constraint
            modelBuilder.Entity<HealthCheckup>()
                .HasIndex(hc => new { hc.CheckupCampaignID, hc.StudentID })
            .IsUnique()
                .HasDatabaseName("UC_StudentCheckup");
        }

        private void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            // Performance indexes
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.SchoolID)
                .HasDatabaseName("IX_Student_SchoolID");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.SchoolID)
                .HasDatabaseName("IX_User_SchoolID");

            modelBuilder.Entity<HealthRecord>()
                .HasIndex(hr => hr.StudentID)
                .HasDatabaseName("IX_HealthRecord_StudentID");

            modelBuilder.Entity<MedicalEvent>()
                .HasIndex(me => me.StudentID)
                .HasDatabaseName("IX_MedicalEvent_StudentID");

            modelBuilder.Entity<MedicineRequest>()
                .HasIndex(mr => mr.StudentID)
                .HasDatabaseName("IX_MedicineRequest_StudentID");

            modelBuilder.Entity<Vaccination>()
                .HasIndex(v => v.StudentID)
                .HasDatabaseName("IX_Vaccination_StudentID");

            modelBuilder.Entity<HealthCheckup>()
                .HasIndex(hc => hc.StudentID)
                .HasDatabaseName("IX_HealthCheckup_StudentID");

            modelBuilder.Entity<Notification>()
            .HasIndex(n => n.UserID)
                .HasDatabaseName("IX_Notification_UserID");
        }

        private void ConfigureEnumConversions(ModelBuilder modelBuilder)
        {
            // Configure enum to string conversions to match SQL Server VARCHAR constraints
            modelBuilder.Entity<User>()
                .Property(u => u.UserType)
                .HasConversion<string>()
                .HasMaxLength(20);

            modelBuilder.Entity<Student>()
                .Property(s => s.Gender)
                .HasConversion<string>()
                .HasMaxLength(10);

            modelBuilder.Entity<MedicineRequest>()
                .Property(mr => mr.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            modelBuilder.Entity<MedicalEvent>()
                .Property(me => me.EventType)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<MedicalEvent>()
                .Property(me => me.Severity)
                .HasConversion<string>()
                .HasMaxLength(20);

            modelBuilder.Entity<VaccinationCampaign>()
                .Property(vc => vc.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            modelBuilder.Entity<HealthCheckupCampaign>()
                .Property(hcc => hcc.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            modelBuilder.Entity<ConsentForm>()
                .Property(cf => cf.FormType)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<ConsentForm>()
                .Property(cf => cf.ConsentStatus)
                .HasConversion<string>()
                .HasMaxLength(20);

            modelBuilder.Entity<BlogPost>()
            .Property(bp => bp.PostType)
            .HasConversion<string>()
                .HasMaxLength(50);
        }

        private void ConfigureDefaultValues(ModelBuilder modelBuilder)
        {
            // Configure default values for entities
            modelBuilder.Entity<School>()
                .Property(s => s.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<School>()
                .Property(s => s.UpdatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Student>()
                .Property(s => s.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Student>()
                .Property(s => s.UpdatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Medicine>()
                .Property(m => m.Quantity)
                .HasDefaultValue(0);

            modelBuilder.Entity<Medicine>()
                .Property(m => m.MinQuantity)
                .HasDefaultValue(5);

            modelBuilder.Entity<MedicineRequest>()
                .Property(mr => mr.Status)
                .HasDefaultValue(RequestStatus.Pending);

            modelBuilder.Entity<MedicalEvent>()
                .Property(me => me.EventDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<MedicalEvent>()
                .Property(me => me.ParentNotified)
                .HasDefaultValue(false);

            modelBuilder.Entity<MedicalEvent>()
                .Property(me => me.FollowUpRequired)
                .HasDefaultValue(false);

            modelBuilder.Entity<VaccinationCampaign>()
                .Property(vc => vc.Status)
                .HasDefaultValue(CampaignStatus.Planned);

            modelBuilder.Entity<HealthCheckupCampaign>()
                .Property(hcc => hcc.Status)
                .HasDefaultValue(CampaignStatus.Planned);

            modelBuilder.Entity<ConsentForm>()
                .Property(cf => cf.ConsentStatus)
                .HasDefaultValue(ConsentStatus.Pending);

            modelBuilder.Entity<BlogPost>()
                .Property(bp => bp.IsPublished)
                .HasDefaultValue(false);

            modelBuilder.Entity<BlogPost>()
                .Property(bp => bp.ViewCount)
                .HasDefaultValue(0);

            modelBuilder.Entity<Notification>()
                .Property(n => n.IsRead)
                .HasDefaultValue(false);

            modelBuilder.Entity<HealthCheckup>()
                .Property(hc => hc.ParentNotified)
                .HasDefaultValue(false);

            modelBuilder.Entity<StudentParent>()
            .Property(sp => sp.IsPrimary)
                .HasDefaultValue(false);
        }

        private void ConfigureDecimalPrecision(ModelBuilder modelBuilder)
        {
            // Configure decimal precision for health measurements
            modelBuilder.Entity<HealthRecord>()
                .Property(hr => hr.Height)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<HealthRecord>()
                .Property(hr => hr.Weight)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<HealthCheckup>()
                .Property(hc => hc.Height)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<HealthCheckup>()
                .Property(hc => hc.Weight)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<HealthCheckup>()
                .Property(hc => hc.BMI)
                .HasColumnType("decimal(5,2)");
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity.GetType().GetProperty("UpdatedDate") != null)
                {
                    entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Added && entry.Entity.GetType().GetProperty("CreatedDate") != null)
                {
                    entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
            }
        }
    }

    // Extension method for service registration
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSchoolHealthDbContext(this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName = "DefaultConnection")
        {
            services.AddDbContext<SchoolHealthDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(connectionStringName),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);

                        sqlOptions.CommandTimeout(60);
                        sqlOptions.MigrationsAssembly(typeof(SchoolHealthDbContext).Assembly.FullName);
                    });

#if DEBUG
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
#endif
            });

            return services;
        }
    }
}
