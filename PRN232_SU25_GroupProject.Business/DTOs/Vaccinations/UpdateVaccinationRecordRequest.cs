using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.Business.DTOs.Vaccinations
{
    public class UpdateVaccinationRecordRequest
    {
        public int StudentId { get; set; }
        public int CampaignId { get; set; }
        public string VaccineType { get; set; }
        public DateTime VaccinationDate { get; set; }
        public string BatchNumber { get; set; }
        public int NurseId { get; set; }
        public string SideEffects { get; set; }
        public VaccinationResult Result { get; set; }
    }
}
