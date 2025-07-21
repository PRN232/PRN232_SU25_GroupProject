namespace PRN232_SU25_GroupProject.Business.DTOs.Reports
{
    public class RecentIncident
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Type { get; set; }
        public string Severity { get; set; }
        public DateTime IncidentDate { get; set; }
    }

}
