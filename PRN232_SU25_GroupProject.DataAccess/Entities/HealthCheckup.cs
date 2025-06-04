using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class HealthCheckup
    {
        [Key]
        public int CheckupID { get; set; }

        [Required]
        public int CheckupCampaignID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public DateTime CheckupDate { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Height { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Weight { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? BMI { get; set; }

        [MaxLength(20)]
        public string BloodPressure { get; set; }

        [MaxLength(20)]
        public string VisionLeft { get; set; }

        [MaxLength(20)]
        public string VisionRight { get; set; }

        [MaxLength(20)]
        public string HearingLeft { get; set; }

        [MaxLength(20)]
        public string HearingRight { get; set; }

        [MaxLength(200)]
        public string OralHealth { get; set; }

        [MaxLength(500)]
        public string OtherTests { get; set; }

        [MaxLength(200)]
        public string OverallHealthStatus { get; set; }

        [MaxLength(1000)]
        public string DoctorNote { get; set; }

        public int? DoctorID { get; set; }
        public bool ParentNotified { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        // Navigation Properties
        [ForeignKey("CheckupCampaignID")]
        public virtual HealthCheckupCampaign CheckupCampaign { get; set; }

        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("DoctorID")]
        public virtual User Doctor { get; set; }
    }
}
