using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/campaigns")]
    public class HealthCheckupCampaignController : ControllerBase
    {
        private readonly IHealthCheckupCampaignService _service;
        public HealthCheckupCampaignController(IHealthCheckupCampaignService service) => _service = service;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllCampaignsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _service.GetCampaignByIdAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCheckupCampaignRequest request)
        {
            var res = await _service.CreateCampaignAsync(request);
            if (!res.Success) return BadRequest(res);
            return CreatedAtAction(nameof(GetById), new { id = res.Data.Id }, res);
        }

        /// <summary>
        /// Cập nhật HealthCheckupCampaign
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /campaigns
        ///     {
        ///        "id": 2,
        ///        "campaignName": "Test Update Campaign",
        ///        "checkupTypes": "Không có",
        ///        "scheduledDate": "2025-11-03T06:04:33.573Z",
        ///        "targetGrades": "Khối 9",
        ///        "status": 2
        ///     }
        ///
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

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _service.DeleteCampaignAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpGet("{campaignId}/students")]
        [Authorize]
        public async Task<IActionResult> GetScheduledStudents(int campaignId)
            => Ok(await _service.GetScheduledStudentsAsync(campaignId));
    }

}
