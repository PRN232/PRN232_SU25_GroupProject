using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.Allergy;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

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

        /// <summary>
        /// Lấy danh sách dị ứng theo hồ sơ y tế.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế.</param>
        /// <returns>Danh sách dị ứng liên quan.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(int profileId)
        {
            var res = await _allergyService.GetAllergiesByProfileAsync(profileId);
            return Ok(res);
        }

        /// <summary>
        /// Lấy chi tiết dị ứng theo ID.
        /// </summary>
        /// <param name="allergyId">ID dị ứng.</param>
        /// <returns>Thông tin chi tiết dị ứng.</returns>
        [HttpGet("{allergyId}")]
        public async Task<IActionResult> GetById(int allergyId)
        {
            var res = await _allergyService.GetAllergyByIdAsync(allergyId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Tạo mới dị ứng cho hồ sơ y tế.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế.</param>
        /// <param name="request">Thông tin dị ứng cần tạo.</param>
        /// <returns>Kết quả tạo mới.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(int profileId, [FromBody] CreateAllergyRequest request)
        {
            request.MedicalProfileId = profileId;
            var res = await _allergyService.CreateAllergyAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        /// <summary>
        /// Cập nhật thông tin dị ứng.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế.</param>
        /// <param name="allergyId">ID dị ứng cần cập nhật.</param>
        /// <param name="request">Thông tin dị ứng mới.</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPut("{allergyId}")]
        public async Task<IActionResult> Update(int profileId, int allergyId, [FromBody] UpdateAllergyRequest request)
        {
            if (allergyId != request.Id)
                return BadRequest(ApiResponse<object>.ErrorResult("Id không khớp."));

            request.MedicalProfileId = profileId;
            var res = await _allergyService.UpdateAllergyAsync(request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Xoá dị ứng theo ID.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế.</param>
        /// <param name="allergyId">ID dị ứng cần xoá.</param>
        /// <returns>Kết quả xoá.</returns>
        [HttpDelete("{allergyId}")]
        public async Task<IActionResult> Delete(int profileId, int allergyId)
        {
            var res = await _allergyService.DeleteAllergyAsync(allergyId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }
    }
}
