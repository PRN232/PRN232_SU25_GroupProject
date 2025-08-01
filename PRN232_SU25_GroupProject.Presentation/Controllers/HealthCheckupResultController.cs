﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Dtos.HealthCheckups;
using PRN232_SU25_GroupProject.Business.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/results")]
    public class HealthCheckupResultController : ControllerBase
    {
        private readonly IHealthCheckupResultService _service;
        public HealthCheckupResultController(IHealthCheckupResultService service) => _service = service;

        /// <summary>
        /// Ghi nhận kết quả khám sức khỏe từ trường (y tá/giáo viên).
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecordCheckupRequest request)
        {
            var res = await _service.RecordCheckupResultAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        /// <summary>
        /// Ghi nhận kết quả khám sức khỏe từ phụ huynh.
        /// </summary>
        [HttpPost("nurse")]
        public async Task<IActionResult> CreateByParent([FromBody] RecordCheckupRequestParent request)
        {
            var res = await _service.RecordCheckupResultParentAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        /// <summary>
        /// Lấy chi tiết kết quả khám sức khỏe theo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _service.GetResultByIdAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Cập nhật kết quả khám sức khỏe.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCheckupResultRequest request)
        {
            if (id != request.Id) return BadRequest(ApiResponse<object>.ErrorResult("Id không khớp."));
            var res = await _service.UpdateResultAsync(request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Xóa kết quả khám sức khỏe theo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _service.DeleteResultAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Lấy danh sách kết quả khám theo chiến dịch.
        /// </summary>
        [HttpGet("by-campaign/{campaignId}")]
        public async Task<IActionResult> GetResultsByCampaign(int campaignId)
            => Ok(await _service.GetResultsByCampaignAsync(campaignId));

        /// <summary>
        /// Lấy kết quả khám của học sinh. (Chỉ phụ huynh có quyền xem của con họ.)
        /// </summary>
        [HttpGet("by-student/{studentId}")]
        [Authorize]
        public async Task<IActionResult> GetResultsByStudent(int studentId)
        {
            string userIdStr = User.FindFirst("id")?.Value;
            int? userId = userIdStr != null ? int.Parse(userIdStr) : (int?)null;
            string role = User.FindFirst("role")?.Value;
            var res = await _service.GetResultsByStudentAsync(studentId, userId, role);
            return Ok(res);
        }
    }
}
