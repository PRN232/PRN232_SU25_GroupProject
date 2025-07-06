using PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Medications;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Reports
{
    public class StudentHealthSummary
    {
        public StudentDto Student { get; set; }
        public MedicalProfileDto MedicalProfile { get; set; }
        public List<MedicalIncidentDto> RecentIncidents { get; set; } = new List<MedicalIncidentDto>();
        public List<VaccinationRecordDto> VaccinationHistory { get; set; } = new List<VaccinationRecordDto>();
        public List<HealthCheckupResultDto> CheckupHistory { get; set; } = new List<HealthCheckupResultDto>();
        public List<StudentMedicationDto> CurrentMedications { get; set; } = new List<StudentMedicationDto>();
    }
}
