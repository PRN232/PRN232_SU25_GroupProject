using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.ChronicDisease;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

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

        /// <summary>
        /// Lấy danh sách bệnh mãn tính theo hồ sơ y tế.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế.</param>
        [HttpGet]
        public async Task<IActionResult> GetAll(int profileId)
        {
            var res = await _chronicDiseaseService.GetChronicDiseasesByProfileAsync(profileId);
            return Ok(res);
        }

        /// <summary>
        /// Lấy chi tiết một bệnh mãn tính theo ID.
        /// </summary>
        /// <param name="chronicDiseaseId">ID bệnh mãn tính.</param>
        [HttpGet("{chronicDiseaseId}")]
        public async Task<IActionResult> GetById(int chronicDiseaseId)
        {
            var res = await _chronicDiseaseService.GetChronicDiseaseByIdAsync(chronicDiseaseId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Tạo mới một bệnh mãn tính cho hồ sơ y tế.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế.</param>
        /// <param name="request">Thông tin bệnh mãn tính cần tạo.</param>
        [HttpPost]
        public async Task<IActionResult> Create(int profileId, [FromBody] CreateChronicDiseaseRequest request)
        {
            request.MedicalProfileId = profileId;
            var res = await _chronicDiseaseService.CreateChronicDiseaseAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        /// <summary>
        /// Cập nhật thông tin bệnh mãn tính.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế.</param>
        /// <param name="chronicDiseaseId">ID bệnh mãn tính.</param>
        /// <param name="request">Thông tin cần cập nhật.</param>
        [HttpPut("{chronicDiseaseId}")]
        public async Task<IActionResult> Update(int profileId, int chronicDiseaseId, [FromBody] UpdateChronicDiseaseRequest request)
        {
            if (chronicDiseaseId != request.Id) return BadRequest(ApiResponse<object>.ErrorResult("Id không khớp."));
            request.MedicalProfileId = profileId;
            var res = await _chronicDiseaseService.UpdateChronicDiseaseAsync(request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Xoá một bệnh mãn tính khỏi hồ sơ y tế.
        /// </summary>
        /// <param name="profileId">ID hồ sơ y tế.</param>
        /// <param name="chronicDiseaseId">ID bệnh mãn tính.</param>
        [HttpDelete("{chronicDiseaseId}")]
        public async Task<IActionResult> Delete(int profileId, int chronicDiseaseId)
        {
            var res = await _chronicDiseaseService.DeleteChronicDiseaseAsync(chronicDiseaseId);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }
    }


}
