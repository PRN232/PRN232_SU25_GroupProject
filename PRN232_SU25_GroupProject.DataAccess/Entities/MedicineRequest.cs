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
    public class MedicineRequest
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int ParentID { get; set; }

        [Required]
        [MaxLength(100)]
        public string MedicineName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Dosage { get; set; }

        [Required]
        [MaxLength(500)]
        public string UsageInstruction { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        [MaxLength(500)]
        public string RejectReason { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("ParentID")]
        public virtual User Parent { get; set; }

        [ForeignKey("ApprovedBy")]
        public virtual User ApprovedByUser { get; set; }
    }
}
