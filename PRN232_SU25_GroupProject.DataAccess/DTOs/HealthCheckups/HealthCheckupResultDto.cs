namespace PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups
{
    public class HealthCheckupResultDto
    {
        public HealthCheckupResultDto() { }
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal BMI => Height > 0 ? Weight / ((Height / 100) * (Height / 100)) : 0;
        public string BloodPressure { get; set; }
        public string VisionTest { get; set; }
        public string HearingTest { get; set; }
        public string GeneralHealth { get; set; }
        public bool RequiresFollowup { get; set; }
        public string Recommendations { get; set; }
        public DateTime CheckupDate { get; set; }
        public int NurseId { get; set; }
        public string NurseName { get; set; }
    }
}
