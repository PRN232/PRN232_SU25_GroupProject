using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/campaigns")]
    public class HealthCheckupCampaignController : ControllerBase
    {
        private readonly IHealthCheckupCampaignService _service;
        public HealthCheckupCampaignController(IHealthCheckupCampaignService service) => _service = service;

        /// <summary>
        /// Lấy tất cả các chiến dịch khám sức khỏe.
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllCampaignsAsync());

        /// <summary>
        /// Lấy thông tin chi tiết của một chiến dịch khám sức khỏe theo ID.
        /// </summary>
        /// <param name="id">ID của chiến dịch</param>
        /// <returns>Thông tin chiến dịch</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _service.GetCampaignByIdAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Tạo một chiến dịch khám sức khỏe mới.
        /// </summary>
        /// <param name="request">Thông tin chiến dịch cần tạo</param>
        /// <returns>Chiến dịch vừa được tạo</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCheckupCampaignRequest request)
        {
            var res = await _service.CreateCampaignAsync(request);
            if (!res.Success) return BadRequest(res);
            return CreatedAtAction(nameof(GetById), new { id = res.Data.Id }, res);
        }

        /// <summary>
        /// Cập nhật thông tin một chiến dịch khám sức khỏe.
        /// </summary>
        /// <param name="id">ID chiến dịch cần cập nhật</param>
        /// <param name="request">Dữ liệu cập nhật</param>
        /// <returns>Kết quả cập nhật</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /campaigns/1
        ///     {
        ///        "id": 1,
        ///        "campaignName": "Khám sức khỏe tổng quát",
        ///        "checkupTypes": "Răng miệng, Thị lực",
        ///        "scheduledDate": "2025-11-03T06:04:33.573Z",
        ///        "targetGrades": "Khối 9",
        ///        "status": 1
        ///     }
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCheckupCampaignRequest request)
        {
            if (id != request.Id) return BadRequest(ApiResponse<object>.ErrorResult("Id không khớp."));
            var res = await _service.UpdateCampaignAsync(request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Xóa một chiến dịch khám sức khỏe.
        /// </summary>
        /// <param name="id">ID của chiến dịch cần xóa</param>
        /// <returns>Kết quả xóa</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _service.DeleteCampaignAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Lấy danh sách học sinh đã được xếp lịch cho một chiến dịch khám sức khỏe.
        /// </summary>
        /// <param name="campaignId">ID của chiến dịch</param>
        /// <returns>Danh sách học sinh</returns>
        [HttpGet("{campaignId}/students")]
        [Authorize]
        public async Task<IActionResult> GetScheduledStudents(int campaignId)
            => Ok(await _service.GetScheduledStudentsAsync(campaignId));

    }

}
