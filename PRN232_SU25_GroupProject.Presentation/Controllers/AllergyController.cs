using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.Allergy;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/medicalprofiles/{profileId}/allergies")]
    public class AllergyController : ControllerBase
    {
        private readonly IAllergyService _allergyService;

        public AllergyController(IAllergyService allergyService)
        {
            _allergyService = allergyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int profileId)
        {
            var res = await _allergyService.GetAllergiesByProfileAsync(profileId);
            return Ok(res);
        }

        [HttpGet("{allergyId}")]
        public async Task<IActionResult> GetById(int allergyId)
        {
            var res = await _allergyService.GetAllergyByIdAsync(allergyId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int profileId, [FromBody] CreateAllergyRequest request)
        {
            request.MedicalProfileId = profileId;
            var res = await _allergyService.CreateAllergyAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("{allergyId}")]
        public async Task<IActionResult> Update(int profileId, int allergyId, [FromBody] UpdateAllergyRequest request)
        {
            if (allergyId != request.Id) return BadRequest(ApiResponse<object>.ErrorResult("Id không khớp."));
            request.MedicalProfileId = profileId;
            var res = await _allergyService.UpdateAllergyAsync(request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpDelete("{allergyId}")]
        public async Task<IActionResult> Delete(int profileId, int allergyId)
        {
            var res = await _allergyService.DeleteAllergyAsync(allergyId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }
    }

}
