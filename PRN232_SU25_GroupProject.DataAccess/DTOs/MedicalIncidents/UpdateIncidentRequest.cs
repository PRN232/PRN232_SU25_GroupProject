using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents
{
    public class UpdateIncidentRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
        public IncidentSeverity Severity { get; set; }
        public bool ParentNotified { get; set; }
    }
}
