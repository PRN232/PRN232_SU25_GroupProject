using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalProfileController : ControllerBase
    {
        private readonly IMedicalProfileService _medicalProfileService;

        public MedicalProfileController(IMedicalProfileService medicalProfileService)
        {
            _medicalProfileService = medicalProfileService;
        }


        [HttpGet("by-student/{studentId}")]
        public async Task<IActionResult> GetByStudentId(int studentId)
        {
            var res = await _medicalProfileService.GetByStudentIdAsync(studentId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateMedicalProfileRequest request)
        //{
        //    var res = await _medicalProfileService.UpdateAsync(request);
        //    if (!res.Success) return NotFound(res);
        //    return Ok(res);
        //}
    }
}
