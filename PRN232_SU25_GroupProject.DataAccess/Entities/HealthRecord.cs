using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class HealthRecord
    {
        [Key]
        public int HealthRecordID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Height { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Weight { get; set; }

        [MaxLength(10)]
        public string BloodType { get; set; }

        [MaxLength(500)]
        public string AllergyInfo { get; set; }

        [MaxLength(500)]
        public string ChronicDiseases { get; set; }

        [MaxLength(200)]
        public string VisionInfo { get; set; }

        [MaxLength(200)]
        public string HearingInfo { get; set; }

        [MaxLength(1000)]
        public string OtherHealthInfo { get; set; }

        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }

        // Navigation Properties
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser { get; set; }
    }
}
