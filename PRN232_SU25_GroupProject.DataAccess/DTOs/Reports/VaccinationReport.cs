using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Reports
{
    public class VaccinationReport
    {
        public string CampaignName { get; set; }
        public int TotalEligible { get; set; }
        public int ConsentReceived { get; set; }
        public int VaccinationsCompleted { get; set; }
        public int VaccinationsDeferred { get; set; }
        public double CompletionRate { get; set; }
        public List<VaccinationRecordDto> Details { get; set; } = new List<VaccinationRecordDto>();
    }
}
