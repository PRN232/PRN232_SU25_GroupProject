using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;

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

        /// <summary>
        /// Lấy hồ sơ y tế của học sinh theo ID học sinh.
        /// </summary>
        /// <param name="studentId">ID của học sinh.</param>
        /// <returns>Thông tin hồ sơ y tế tương ứng.</returns>
        [HttpGet("by-student/{studentId}")]
        public async Task<IActionResult> GetByStudentId(int studentId)
        {
            var res = await _medicalProfileService.GetByStudentIdAsync(studentId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        //// <summary>
        //// Cập nhật hồ sơ y tế của học sinh.
        //// </summary>
        //// <param name="request">Thông tin cần cập nhật.</param>
        //// <returns>Thông tin hồ sơ sau khi cập nhật.</returns>
        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateMedicalProfileRequest request)
        //{
        //    var res = await _medicalProfileService.UpdateAsync(request);
        //    if (!res.Success) return NotFound(res);
        //    return Ok(res);
        //}
    }
}
