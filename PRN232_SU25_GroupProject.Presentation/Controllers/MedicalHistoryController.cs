using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.MedicalHistory;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/medicalprofiles/{profileId}/medical-histories")]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly IMedicalHistoryService _medicalHistoryService;

        public MedicalHistoryController(IMedicalHistoryService medicalHistoryService)
        {
            _medicalHistoryService = medicalHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int profileId)
        {
            var res = await _medicalHistoryService.GetMedicalHistoriesByProfileAsync(profileId);
            return Ok(res);
        }

        [HttpGet("{medicalHistoryId}")]
        public async Task<IActionResult> GetById(int medicalHistoryId)
        {
            var res = await _medicalHistoryService.GetMedicalHistoryByIdAsync(medicalHistoryId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int profileId, [FromBody] CreateMedicalHistoryRequest request)
        {
            request.MedicalProfileId = profileId;
            var res = await _medicalHistoryService.CreateMedicalHistoryAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("{medicalHistoryId}")]
        public async Task<IActionResult> Update(int profileId, int medicalHistoryId, [FromBody] UpdateMedicalHistoryRequest request)
        {
            if (medicalHistoryId != request.Id) return BadRequest(ApiResponse<object>.ErrorResult("Id không khớp."));
            request.MedicalProfileId = profileId;
            var res = await _medicalHistoryService.UpdateMedicalHistoryAsync(request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        [HttpDelete("{medicalHistoryId}")]
        public async Task<IActionResult> Delete(int profileId, int medicalHistoryId)
        {
            var res = await _medicalHistoryService.DeleteMedicalHistoryAsync(medicalHistoryId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }
    }

}
