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
    public class HealthCheckupCampaign
    {
        [Key]
        public int CheckupCampaignID { get; set; }

        [Required]
        [MaxLength(100)]
        public string CampaignName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public int SchoolID { get; set; }

        public CampaignStatus Status { get; set; } = CampaignStatus.Planned;

        [Required]
        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [ForeignKey("SchoolID")]
        public virtual School School { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser { get; set; }
        public virtual ICollection<HealthCheckup> HealthCheckups { get; set; } = new HashSet<HealthCheckup>();
    }
}
