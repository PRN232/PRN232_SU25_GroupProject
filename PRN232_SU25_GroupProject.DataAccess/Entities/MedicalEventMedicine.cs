using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class MedicalEventMedicine
    {
        [Key]
        public int EventMedicineID { get; set; }

        [Required]
        public int EventID { get; set; }

        public int? MedicineID { get; set; }

        [Required]
        [MaxLength(100)]
        public string MedicineName { get; set; }

        [Required]
        public int QuantityUsed { get; set; }

        [MaxLength(100)]
        public string Dosage { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("EventID")]
        public virtual MedicalEvent MedicalEvent { get; set; }

        [ForeignKey("MedicineID")]
        public virtual Medicine Medicine { get; set; }
    }
}
