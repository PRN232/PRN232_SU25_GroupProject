using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.Allergy;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.ChronicDisease;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.MedicalHistory;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles
{
    public class MedicalProfileDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime LastUpdated { get; set; }

        public List<AllergyDto> Allergies { get; set; } = new List<AllergyDto>();
        public List<ChronicDiseaseDto> ChronicDiseases { get; set; } = new List<ChronicDiseaseDto>();
        public List<MedicalHistoryDto> MedicalHistories { get; set; } = new List<MedicalHistoryDto>();
        public List<VaccinationRecordDto> VaccinationRecords { get; set; } = new List<VaccinationRecordDto>();

        // Thêm các trường thông tin sức khỏe từ lần khám gần nhất:
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string BloodPressure { get; set; }
        public string VisionTest { get; set; }
        public string HearingTest { get; set; }
        public string GeneralHealth { get; set; }
        public bool? RequiresFollowup { get; set; }
        public string Recommendations { get; set; }
        public DateTime? LastCheckupDate { get; set; }
    }

}
