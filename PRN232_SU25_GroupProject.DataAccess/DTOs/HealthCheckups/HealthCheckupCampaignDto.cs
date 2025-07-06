using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups
{
    public class HealthCheckupCampaignDto
    {
        public HealthCheckupCampaignDto() { }
        public int Id { get; set; }
        public string CampaignName { get; set; }
        public string CheckupTypes { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string TargetGrades { get; set; }
        public CheckupStatus Status { get; set; }
        public string StatusDisplay => Status.ToString();
        public int TotalStudents { get; set; }
        public int ConsentReceived { get; set; }
        public int CheckupsCompleted { get; set; }
        public int RequiringFollowup { get; set; }
        public double CompletionRate => TotalStudents > 0 ? (double)CheckupsCompleted / TotalStudents * 100 : 0;
        public double ConsentRate => TotalStudents > 0 ? (double)ConsentReceived / TotalStudents * 100 : 0;
    }
}
