using System.ComponentModel.DataAnnotations;

namespace PRN232_SU25_GroupProject.Business.DTOs.HealthCheckups
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
