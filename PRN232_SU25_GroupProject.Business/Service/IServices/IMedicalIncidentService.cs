using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IMedicalIncidentService
    {
        Task<ApiResponse<List<MedicalIncidentDto>>> GetAllIncidentsAsync();
        Task<ApiResponse<MedicalIncidentDto>> GetIncidentByIdAsync(int id);
        Task<ApiResponse<MedicalIncidentDto>> CreateIncidentAsync(CreateMedicalIncidentRequest request);
        Task<ApiResponse<MedicalIncidentDto>> UpdateIncidentAsync(int id, UpdateMedicalIncidentRequest request);
        Task<ApiResponse<bool>> DeleteIncidentAsync(int id);
    }
}
