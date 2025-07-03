using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups
{
    public class CreateCheckupCampaignRequest
    {
        [Required]
        public string CampaignName { get; set; }

        [Required]
        public string CheckupTypes { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public string TargetGrades { get; set; }


    }
}
