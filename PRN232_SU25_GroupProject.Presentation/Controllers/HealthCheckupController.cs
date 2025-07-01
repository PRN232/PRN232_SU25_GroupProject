using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
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

        // Lấy tất cả các đợt kiểm tra sức khỏe
        [HttpGet("campaigns")]
        [Authorize]
        public async Task<IActionResult> GetAllCampaigns()
        {
            var result = await _healthCheckupService.GetAllCampaignsAsync();
            return Ok(result);
        }

        // Tạo mới 1 đợt kiểm tra sức khỏe
        [HttpPost("campaigns")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCheckupCampaignRequest request)
        {
            var result = await _healthCheckupService.CreateCampaignAsync(request);
            return CreatedAtAction(nameof(GetCampaignById), new { id = result.Id }, result);
        }

        // Lấy thông tin đợt kiểm tra theo ID
        [HttpGet("campaigns/{id}")]
        [Authorize]
        public async Task<IActionResult> GetCampaignById(int id)
        {
            try
            {
                var result = await _healthCheckupService.GetCampaignByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // Lấy danh sách học sinh được kiểm tra trong đợt kiểm tra sức khỏe
        [HttpGet("campaigns/{campaignId}/students")]
        [Authorize]
        public async Task<IActionResult> GetScheduledStudents(int campaignId)
        {
            var result = await _healthCheckupService.GetScheduledStudentsAsync(campaignId);
            return Ok(result);
        }

        // Gửi thông báo đến phụ huynh về đợt kiểm tra sức khỏe
        [HttpPost("campaigns/{campaignId}/notify-parents")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse")]
        public async Task<IActionResult> SendNotificationToParents(int campaignId)
        {
            var success = await _healthCheckupService.SendNotificationToParentsAsync(campaignId);
            if (success)
                return Ok(new { message = "Gửi thông báo thành công!" });
            return NotFound(new { message = "Không tìm thấy đợt kiểm tra." });
        }

        // Ghi nhận kết quả kiểm tra sức khỏe cho học sinh
        [HttpPost("results")]
        [Authorize(Roles = "SchoolNurse,Manager")]
        public async Task<IActionResult> RecordCheckupResult([FromBody] RecordCheckupRequest request)
        {
            var result = await _healthCheckupService.RecordCheckupResultAsync(request);
            return Ok(result);
        }

        // Xem lịch sử kiểm tra sức khỏe của một học sinh
        [HttpGet("students/{studentId}/history")]
        [Authorize(Roles = "Parent,SchoolNurse,Manager,Admin")]
        public async Task<IActionResult> GetStudentCheckupHistory(int studentId)
        {
            // Nếu là parent thì chỉ được xem lịch sử của con mình
            if (User.IsInRole("Parent"))
            {
                string userId = User.Claims.FirstOrDefault(x => x.Type == "sub" || x.Type == "UserId")?.Value;
                // Lấy student từ DB
                var student = await _healthCheckupService.GetStudentByIdAsync(studentId);
                if (student == null || student.ParentId != int.Parse(userId))
                {
                    return Forbid("Bạn không có quyền truy cập thông tin này.");
                }
            }

            var result = await _healthCheckupService.GetStudentCheckupHistoryAsync(studentId);
            return Ok(result);
        }


        // Gửi kết quả kiểm tra sức khỏe cho phụ huynh
        [HttpPost("results/{resultId}/send-to-parent")]
        [Authorize(Roles = "SchoolNurse,Manager")]
        public async Task<IActionResult> SendResultToParent(int resultId)
        {
            var success = await _healthCheckupService.SendResultToParentAsync(resultId);
            if (success)
                return Ok(new { message = "Gửi kết quả cho phụ huynh thành công!" });
            return NotFound(new { message = "Không tìm thấy kết quả kiểm tra hoặc phụ huynh." });
        }

        // Lên lịch hẹn tư vấn (tái khám) cho học sinh nếu có dấu hiệu bất thường
        [HttpPost("results/{resultId}/schedule-followup")]
        [Authorize(Roles = "SchoolNurse,Manager")]
        public async Task<IActionResult> ScheduleFollowup(int resultId, [FromQuery] DateTime appointmentDate)
        {
            var success = await _healthCheckupService.ScheduleFollowupAsync(resultId, appointmentDate);
            if (success)
                return Ok(new { message = "Đã lên lịch hẹn tái khám." });
            return NotFound(new { message = "Không tìm thấy kết quả kiểm tra." });
        }
    }
}
