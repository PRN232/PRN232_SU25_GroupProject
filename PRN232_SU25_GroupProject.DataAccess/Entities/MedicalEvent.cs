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
    public class MedicalEvent
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public EventType EventType { get; set; }

        [Required]
        public DateTime EventDate { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(500)]
        public string Symptoms { get; set; }

        [MaxLength(500)]
        public string FirstAid { get; set; }

        public Severity? Severity { get; set; }

        [Required]
        public int HandledBy { get; set; }

        public bool ParentNotified { get; set; } = false;
        public DateTime? ParentNotificationDate { get; set; }
        public bool FollowUpRequired { get; set; } = false;

        [MaxLength(500)]
        public string FollowUpInfo { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        // Navigation Properties
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("HandledBy")]
        public virtual User HandledByUser { get; set; }
        public virtual ICollection<MedicalEventMedicine> MedicalEventMedicines { get; set; } = new HashSet<MedicalEventMedicine>();
    }
}
