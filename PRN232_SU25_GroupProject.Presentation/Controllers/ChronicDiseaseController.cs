using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.ChronicDisease;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/medicalprofiles/{profileId}/chronic-diseases")]
    public class ChronicDiseaseController : ControllerBase
    {
        private readonly IChronicDiseaseService _chronicDiseaseService;

        public ChronicDiseaseController(IChronicDiseaseService chronicDiseaseService)
        {
            _chronicDiseaseService = chronicDiseaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int profileId)
        {
            var res = await _chronicDiseaseService.GetChronicDiseasesByProfileAsync(profileId);
            return Ok(res);
        }

        [HttpGet("{chronicDiseaseId}")]
        public async Task<IActionResult> GetById(int chronicDiseaseId)
        {
            var res = await _chronicDiseaseService.GetChronicDiseaseByIdAsync(chronicDiseaseId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int profileId, [FromBody] CreateChronicDiseaseRequest request)
        {
            request.MedicalProfileId = profileId;
            var res = await _chronicDiseaseService.CreateChronicDiseaseAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("{chronicDiseaseId}")]
        public async Task<IActionResult> Update(int profileId, int chronicDiseaseId, [FromBody] UpdateChronicDiseaseRequest request)
        {
            if (chronicDiseaseId != request.Id) return BadRequest(ApiResponse<object>.ErrorResult("Id không khớp."));
            request.MedicalProfileId = profileId;
            var res = await _chronicDiseaseService.UpdateChronicDiseaseAsync(request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpDelete("{chronicDiseaseId}")]
        public async Task<IActionResult> Delete(int profileId, int chronicDiseaseId)
        {
            var res = await _chronicDiseaseService.DeleteChronicDiseaseAsync(chronicDiseaseId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }
    }

}
