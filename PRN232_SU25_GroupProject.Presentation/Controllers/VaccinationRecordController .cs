using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.Business.Service.IServices;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/vaccination-records")]
    public class VaccinationRecordController : ControllerBase
    {
        private readonly IVaccinationRecordService _recordService;
        public VaccinationRecordController(IVaccinationRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _recordService.GetRecordByIdAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpGet("by-campaign/{campaignId}")]
        [Authorize]
        public async Task<IActionResult> GetByCampaign(int campaignId)
        {
            var res = await _recordService.GetRecordsByCampaignAsync(campaignId);
            return Ok(res);
        }

        [HttpGet("by-student/{studentId}")]
        [Authorize]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var res = await _recordService.GetRecordsByStudentAsync(studentId);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "SchoolNurse,Manager,Admin")]
        public async Task<IActionResult> Create([FromBody] CreateVaccinationRecordRequest request)
        {
            var res = await _recordService.CreateRecordAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SchoolNurse,Manager,Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVaccinationRecordRequest request)
        {
            var res = await _recordService.UpdateRecordAsync(id, request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

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

