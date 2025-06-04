using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public UserType UserType { get; set; }

        public int? SchoolID { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [ForeignKey("SchoolID")]
        public virtual School School { get; set; }
        public virtual ICollection<StudentParent> StudentParents { get; set; } = new HashSet<StudentParent>();
        public virtual ICollection<HealthRecord> HealthRecords { get; set; } = new HashSet<HealthRecord>();
        public virtual ICollection<MedicineRequest> MedicineRequests { get; set; } = new HashSet<MedicineRequest>();
        public virtual ICollection<MedicineRequest> ApprovedMedicineRequests { get; set; } = new HashSet<MedicineRequest>();
        public virtual ICollection<MedicalEvent> HandledMedicalEvents { get; set; } = new HashSet<MedicalEvent>();
        public virtual ICollection<VaccinationCampaign> CreatedVaccinationCampaigns { get; set; } = new HashSet<VaccinationCampaign>();
        public virtual ICollection<ConsentForm> ConsentForms { get; set; } = new HashSet<ConsentForm>();
        public virtual ICollection<Vaccination> AdministeredVaccinations { get; set; } = new HashSet<Vaccination>();
        public virtual ICollection<HealthCheckupCampaign> CreatedHealthCheckupCampaigns { get; set; } = new HashSet<HealthCheckupCampaign>();
        public virtual ICollection<HealthCheckup> HealthCheckups { get; set; } = new HashSet<HealthCheckup>();
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new HashSet<BlogPost>();
        public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
        public virtual ICollection<SystemLog> SystemLogs { get; set; } = new HashSet<SystemLog>();
    }
}
