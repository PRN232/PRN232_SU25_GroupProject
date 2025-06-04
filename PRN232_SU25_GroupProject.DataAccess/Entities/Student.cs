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
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [MaxLength(20)]
        public string StudentCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        [Required]
        public int SchoolID { get; set; }

        [MaxLength(255)]
        public string PhotoURL { get; set; }

        [MaxLength(10)]
        public string BloodType { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [ForeignKey("SchoolID")]
        public virtual School School { get; set; }
        public virtual ICollection<StudentParent> StudentParents { get; set; } = new HashSet<StudentParent>();
        public virtual ICollection<HealthRecord> HealthRecords { get; set; } = new HashSet<HealthRecord>();
        public virtual ICollection<MedicineRequest> MedicineRequests { get; set; } = new HashSet<MedicineRequest>();
        public virtual ICollection<MedicalEvent> MedicalEvents { get; set; } = new HashSet<MedicalEvent>();
        public virtual ICollection<ConsentForm> ConsentForms { get; set; } = new HashSet<ConsentForm>();
        public virtual ICollection<Vaccination> Vaccinations { get; set; } = new HashSet<Vaccination>();
        public virtual ICollection<HealthCheckup> HealthCheckups { get; set; } = new HashSet<HealthCheckup>();
    }
}
