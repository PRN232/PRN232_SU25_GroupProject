using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups
{
    public class UpdateCheckupCampaignRequest
    {
        public int Id { get; set; }
        public string CampaignName { get; set; }
        public string CheckupTypes { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string TargetGrades { get; set; }
        public CheckupStatus Status { get; set; }
    }
}
