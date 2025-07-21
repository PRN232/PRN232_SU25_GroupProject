using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.Allergy;
using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.ChronicDisease;
using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.MedicalHistory;
using PRN232_SU25_GroupProject.Business.DTOs.Vaccinations;

namespace PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles
{
    public class UpdateMedicalProfileRequest
    {
        public int StudentId { get; set; }
        public List<AllergyDto> Allergies { get; set; } = new List<AllergyDto>();
        public List<ChronicDiseaseDto> ChronicDiseases { get; set; } = new List<ChronicDiseaseDto>();
        public List<MedicalHistoryDto> MedicalHistories { get; set; } = new List<MedicalHistoryDto>();
        public List<VaccinationRecordDto> VaccinationRecords { get; set; }
    }
}
