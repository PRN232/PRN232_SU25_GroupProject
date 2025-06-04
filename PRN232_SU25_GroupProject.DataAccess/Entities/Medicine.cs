using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class Medicine
    {
        [Key]
        public int MedicineID { get; set; }

        [Required]
        [MaxLength(100)]
        public string MedicineName { get; set; }

        [Required]
        [MaxLength(50)]
        public string MedicineCode { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Unit { get; set; }

        [Required]
        public int Quantity { get; set; } = 0;

        [Required]
        public int MinQuantity { get; set; } = 5;

        public DateTime? ExpiryDate { get; set; }

        [MaxLength(100)]
        public string StorageCondition { get; set; }

        [Required]
        public int SchoolID { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [ForeignKey("SchoolID")]
        public virtual School School { get; set; }
        public virtual ICollection<MedicalEventMedicine> MedicalEventMedicines { get; set; } = new HashSet<MedicalEventMedicine>();
    }
}
