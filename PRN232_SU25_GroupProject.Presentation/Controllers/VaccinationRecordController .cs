using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.Business.Service.IServices;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    /// <summary>
    /// Quản lý các bản ghi tiêm chủng của học sinh.
    /// </summary>
    [ApiController]
    [Route("api/vaccination-records")]
    public class VaccinationRecordController : ControllerBase
    {
        private readonly IVaccinationRecordService _recordService;

        public VaccinationRecordController(IVaccinationRecordService recordService)
        {
            _recordService = recordService;
        }

        /// <summary>
        /// Lấy thông tin chi tiết một bản ghi tiêm chủng theo ID.
        /// </summary>
        /// <param name="id">ID bản ghi tiêm chủng.</param>
        /// <returns>Thông tin bản ghi tiêm chủng.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _recordService.GetRecordByIdAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Lấy danh sách bản ghi tiêm chủng theo chiến dịch.
        /// </summary>
        /// <param name="campaignId">ID chiến dịch tiêm chủng.</param>
        /// <returns>Danh sách bản ghi tiêm chủng.</returns>
        [HttpGet("by-campaign/{campaignId}")]
        [Authorize]
        public async Task<IActionResult> GetByCampaign(int campaignId)
        {
            var res = await _recordService.GetRecordsByCampaignAsync(campaignId);
            return Ok(res);
        }

        /// <summary>
        /// Lấy danh sách bản ghi tiêm chủng theo học sinh.
        /// </summary>
        /// <param name="studentId">ID học sinh.</param>
        /// <returns>Danh sách bản ghi tiêm chủng.</returns>
        [HttpGet("by-student/{studentId}")]
        [Authorize]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var res = await _recordService.GetRecordsByStudentAsync(studentId);
            return Ok(res);
        }

        /// <summary>
        /// Tạo mới bản ghi tiêm chủng cho học sinh.
        /// </summary>
        /// <param name="request">Thông tin bản ghi tiêm chủng cần tạo.</param>
        /// <returns>Kết quả tạo bản ghi.</returns>
        [HttpPost]
        [Authorize(Roles = "SchoolNurse,Manager,Admin")]
        public async Task<IActionResult> Create([FromBody] CreateVaccinationRecordRequest request)
        {
            var res = await _recordService.CreateRecordAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        /// <summary>
        /// Cập nhật thông tin bản ghi tiêm chủng.
        /// </summary>
        /// <param name="id">ID bản ghi tiêm chủng.</param>
        /// <param name="request">Thông tin cập nhật.</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "SchoolNurse,Manager,Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVaccinationRecordRequest request)
        {
            var res = await _recordService.UpdateRecordAsync(id, request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Xoá một bản ghi tiêm chủng.
        /// </summary>
        /// <param name="id">ID bản ghi tiêm chủng cần xoá.</param>
        /// <returns>Kết quả xoá bản ghi.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "SchoolNurse,Manager,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _recordService.DeleteRecordAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }
    }
}
