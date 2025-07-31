using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.MedicalHistory;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/medicalprofiles/{profileId}/medical-histories")]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly IMedicalHistoryService _medicalHistoryService;

        public MedicalHistoryController(IMedicalHistoryService medicalHistoryService)
        {
            _medicalHistoryService = medicalHistoryService;
        }

        /// <summary>
        /// Lấy danh sách tiền sử bệnh theo hồ sơ y tế.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế</param>
        [HttpGet]
        public async Task<IActionResult> GetAll(int profileId)
        {
            var res = await _medicalHistoryService.GetMedicalHistoriesByProfileAsync(profileId);
            return Ok(res);
        }

        /// <summary>
        /// Lấy chi tiết tiền sử bệnh theo ID.
        /// </summary>
        /// <param name="medicalHistoryId">ID tiền sử bệnh</param>
        [HttpGet("{medicalHistoryId}")]
        public async Task<IActionResult> GetById(int medicalHistoryId)
        {
            var res = await _medicalHistoryService.GetMedicalHistoryByIdAsync(medicalHistoryId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Tạo mới tiền sử bệnh cho hồ sơ y tế.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế</param>
        /// <param name="request">Thông tin tiền sử bệnh</param>
        [HttpPost]
        public async Task<IActionResult> Create(int profileId, [FromBody] CreateMedicalHistoryRequest request)
        {
            request.MedicalProfileId = profileId;
            var res = await _medicalHistoryService.CreateMedicalHistoryAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        /// <summary>
        /// Cập nhật tiền sử bệnh theo ID.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế</param>
        /// <param name="medicalHistoryId">ID tiền sử bệnh</param>
        /// <param name="request">Thông tin cập nhật</param>
        [HttpPut("{medicalHistoryId}")]
        public async Task<IActionResult> Update(int profileId, int medicalHistoryId, [FromBody] UpdateMedicalHistoryRequest request)
        {
            if (medicalHistoryId != request.Id)
                return BadRequest(ApiResponse<object>.ErrorResult("Id không khớp."));

            request.MedicalProfileId = profileId;
            var res = await _medicalHistoryService.UpdateMedicalHistoryAsync(request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Xoá tiền sử bệnh theo ID.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế</param>
        /// <param name="medicalHistoryId">ID tiền sử bệnh</param>
        [HttpDelete("{medicalHistoryId}")]
        public async Task<IActionResult> Delete(int profileId, int medicalHistoryId)
        {
            var res = await _medicalHistoryService.DeleteMedicalHistoryAsync(medicalHistoryId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }
    }
}
