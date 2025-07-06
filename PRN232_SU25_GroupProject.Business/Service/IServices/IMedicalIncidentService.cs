using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IMedicalIncidentService
    {
        Task<MedicalIncidentDto> CreateIncidentAsync(CreateIncidentRequest request);
        Task<MedicalIncidentDto> GetIncidentByIdAsync(int id);
        Task<List<MedicalIncidentDto>> GetIncidentsByStudentAsync(int studentId);
        Task<List<MedicalIncidentDto>> GetIncidentsByDateRangeAsync(DateTime from, DateTime to);
        Task<bool> UpdateIncidentAsync(UpdateIncidentRequest request);
        Task<bool> NotifyParentAsync(int incidentId);
    }
}
