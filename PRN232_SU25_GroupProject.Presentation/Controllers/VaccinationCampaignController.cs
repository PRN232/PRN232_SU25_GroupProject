using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.Business.Service.IServices;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/vaccination-campaigns")]
    public class VaccinationCampaignController : ControllerBase
    {
        private readonly IVaccinationCampaignService _campaignService;
        public VaccinationCampaignController(IVaccinationCampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Admin,Parent,SchoolNurse")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _campaignService.GetAllCampaignsAsync();
            return Ok(res);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _campaignService.GetCampaignByIdAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Manager,Admin,SchoolNurse")]
        public async Task<IActionResult> Create([FromBody] CreateVaccinationCampaignRequest request)
        {
            var res = await _campaignService.CreateCampaignAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVaccinationCampaignRequest request)
        {
            var res = await _campaignService.UpdateCampaignAsync(id, request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _campaignService.DeleteCampaignAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpGet("{id}/students")]
        [Authorize]
        public async Task<IActionResult> GetVaccinatedStudents(int id)
        {
            var res = await _campaignService.GetVaccinatedStudentsAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }
    }
}
