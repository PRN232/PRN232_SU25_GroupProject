using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles
{
    public class MedicalProfileDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<AllergyDto> Allergies { get; set; } = new List<AllergyDto>();
        public List<ChronicDiseaseDto> ChronicDiseases { get; set; } = new List<ChronicDiseaseDto>();
        public List<MedicalHistoryDto> MedicalHistories { get; set; } = new List<MedicalHistoryDto>();
        public VisionHearingDto VisionHearing { get; set; }
        public List<VaccinationRecordDto> VaccinationRecords { get; set; } = new List<VaccinationRecordDto>();
    }
}
