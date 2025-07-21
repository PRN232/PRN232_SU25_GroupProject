using PRN232_SU25_GroupProject.Business.DTOs.HealthCheckups;

namespace PRN232_SU25_GroupProject.Business.DTOs.Reports
{
    public class HealthCheckupReport
    {
        public string CampaignName { get; set; }
        public int TotalStudents { get; set; }
        public int CheckupsCompleted { get; set; }
        public int RequiringFollowup { get; set; }
        public Dictionary<string, double> AverageMetrics { get; set; } = new Dictionary<string, double>();
        public List<HealthCheckupResultDto> Details { get; set; } = new List<HealthCheckupResultDto>();
    }
}
