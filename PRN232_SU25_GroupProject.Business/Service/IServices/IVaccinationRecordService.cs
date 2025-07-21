using PRN232_SU25_GroupProject.Business.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IVaccinationRecordService
    {
        Task<ApiResponse<VaccinationRecordDto>> CreateRecordAsync(CreateVaccinationRecordRequest request);
        Task<ApiResponse<VaccinationRecordDto>> GetRecordByIdAsync(int id);
        Task<ApiResponse<List<VaccinationRecordDto>>> GetRecordsByCampaignAsync(int campaignId);
        Task<ApiResponse<List<VaccinationRecordDto>>> GetRecordsByStudentAsync(int studentId);
        Task<ApiResponse<VaccinationRecordDto>> UpdateRecordAsync(int recordId, UpdateVaccinationRecordRequest request);
        Task<ApiResponse<bool>> DeleteRecordAsync(int id);
    }

}
