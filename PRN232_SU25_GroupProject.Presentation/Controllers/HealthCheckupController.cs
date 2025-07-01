using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckupController : ControllerBase
    {
        private readonly IHealthCheckupService _healthCheckupService;

        public HealthCheckupController(IHealthCheckupService healthCheckupService)
        {
            _healthCheckupService = healthCheckupService;
        }

        [HttpGet("campaigns")]
        [Authorize]
        public async Task<IActionResult> GetAllCampaigns()
        {
            var response = await _healthCheckupService.GetAllCampaignsAsync();
            if (!response.Success)
                return BadRequest(response); // Lỗi hệ thống
            return Ok(response);
        }

        [HttpPost("campaigns")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCheckupCampaignRequest request)
        {
            var response = await _healthCheckupService.CreateCampaignAsync(request);
            if (!response.Success)
                return BadRequest(response);
            // Trả về cả ApiResponse và link tới action GetCampaignById
            return CreatedAtAction(nameof(GetCampaignById), new { id = response.Data.Id }, response);
        }

        [HttpGet("campaigns/{id}")]
        [Authorize]
        public async Task<IActionResult> GetCampaignById(int id)
        {
            var response = await _healthCheckupService.GetCampaignByIdAsync(id);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpGet("campaigns/{campaignId}/students")]
        [Authorize]
        public async Task<IActionResult> GetScheduledStudents(int campaignId)
        {
            var response = await _healthCheckupService.GetScheduledStudentsAsync(campaignId);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost("campaigns/{campaignId}/notify-parents")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse")]
        public async Task<IActionResult> SendNotificationToParents(int campaignId)
        {
            var response = await _healthCheckupService.SendNotificationToParentsAsync(campaignId);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost("results")]
        [Authorize(Roles = "SchoolNurse,Manager")]
        public async Task<IActionResult> RecordCheckupResult([FromBody] RecordCheckupRequest request)
        {
            var response = await _healthCheckupService.RecordCheckupResultAsync(request);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("students/{studentId}/history")]
        [Authorize(Roles = "Parent,SchoolNurse,Manager,Admin")]
        public async Task<IActionResult> GetStudentCheckupHistory(int studentId)
        {
            string userIdStr = User.Claims.FirstOrDefault(x => x.Type == "sub" || x.Type == "uid" || x.Type == "UserId")?.Value;
            int? userId = null;
            if (int.TryParse(userIdStr, out var tmpId))
                userId = tmpId;
            string role = User.Claims.FirstOrDefault(x =>
                x.Type == "role" || x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

            var response = await _healthCheckupService.GetStudentCheckupHistoryAsync(studentId, userId, role);

            if (!response.Success)
            {
                if (response.Message.Contains("quyền"))
                    return StatusCode(403, ApiResponse<object>.ErrorResult("Bạn không có quyền truy cập thông tin học sinh này."));
                if (response.Message.Contains("Không tìm thấy") || response.Message.Contains("chưa có lịch sử"))
                    return NotFound(response);
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("results/{resultId}/send-to-parent")]
        [Authorize(Roles = "SchoolNurse,Manager")]
        public async Task<IActionResult> SendResultToParent(int resultId)
        {
            var response = await _healthCheckupService.SendResultToParentAsync(resultId);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost("results/{resultId}/schedule-followup")]
        [Authorize(Roles = "SchoolNurse,Manager")]
        public async Task<IActionResult> ScheduleFollowup(int resultId, [FromQuery] DateTime appointmentDate)
        {
            var response = await _healthCheckupService.ScheduleFollowupAsync(resultId, appointmentDate);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }
    }

}
