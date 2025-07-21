using PRN232_SU25_GroupProject.Business.DTOs.Medications;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IMedicationService
    {
        /// <summary>
        /// Thêm mới một loại thuốc
        /// </summary>
        /// <param name="request">Thông tin thuốc cần thêm</param>
        /// <returns>Trả về đối tượng thuốc vừa được thêm</returns>
        Task<ApiResponse<MedicationDto>> AddMedicationAsync(AddMedicationRequest request);

        /// <summary>
        /// Cập nhật thông tin thuốc
        /// </summary>
        /// <param name="id">ID thuốc cần cập nhật</param>
        /// <param name="request">Thông tin thuốc mới</param>
        /// <returns>Trả về đối tượng thuốc đã được cập nhật</returns>
        Task<ApiResponse<MedicationDto>> UpdateMedicationAsync(int id, UpdateMedicationRequest request);

        /// <summary>
        /// Xóa thuốc khỏi hệ thống
        /// </summary>
        /// <param name="id">ID thuốc cần xóa</param>
        /// <returns>Trả về kết quả thao tác xóa</returns>
        Task<ApiResponse<bool>> DeleteMedicationAsync(int id);

        /// <summary>
        /// Lấy danh sách thuốc trong hệ thống
        /// </summary>
        /// <returns>Trả về danh sách thuốc</returns>
        Task<ApiResponse<List<MedicationDto>>> GetAllMedicationsAsync();

        /// <summary>
        /// Lấy thông tin chi tiết một loại thuốc
        /// </summary>
        /// <param name="id">ID thuốc cần lấy thông tin</param>
        /// <returns>Trả về thông tin thuốc chi tiết</returns>
        Task<ApiResponse<MedicationDto>> GetMedicationByIdAsync(int id);
    }


}
