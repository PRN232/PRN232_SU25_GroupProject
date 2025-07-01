using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class VaccinationCampaign
    {
        public int Id { get; set; }
        public string CampaignName { get; set; }
        public string VaccineType { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string TargetGrades { get; set; }
        public VaccinationStatus Status { get; set; }

    }
}
