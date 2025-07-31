using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.Business.Service.IServices;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    /// <summary>
    /// Quản lý các chiến dịch tiêm chủng.
    /// </summary>
    [ApiController]
    [Route("api/vaccination-campaigns")]
    public class VaccinationCampaignController : ControllerBase
    {
        private readonly IVaccinationCampaignService _campaignService;

        public VaccinationCampaignController(IVaccinationCampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        /// <summary>
        /// Lấy danh sách tất cả chiến dịch tiêm chủng.
        /// </summary>
        /// <returns>Danh sách chiến dịch tiêm chủng.</returns>
        [HttpGet]
        [Authorize(Roles = "Manager,Admin,Parent,SchoolNurse")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _campaignService.GetAllCampaignsAsync();
            return Ok(res);
        }

        /// <summary>
        /// Lấy thông tin chi tiết một chiến dịch tiêm chủng theo ID.
        /// </summary>
        /// <param name="id">ID chiến dịch tiêm chủng.</param>
        /// <returns>Thông tin chiến dịch tiêm chủng.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _campaignService.GetCampaignByIdAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Tạo mới một chiến dịch tiêm chủng.
        /// </summary>
        /// <param name="request">Thông tin chiến dịch cần tạo.</param>
        /// <returns>Kết quả tạo chiến dịch.</returns>
        [HttpPost]
        [Authorize(Roles = "Manager,Admin,SchoolNurse")]
        public async Task<IActionResult> Create([FromBody] CreateVaccinationCampaignRequest request)
        {
            var res = await _campaignService.CreateCampaignAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        /// <summary>
        /// Cập nhật thông tin một chiến dịch tiêm chủng.
        /// </summary>
        /// <param name="id">ID chiến dịch.</param>
        /// <param name="request">Thông tin cần cập nhật.</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVaccinationCampaignRequest request)
        {
            var res = await _campaignService.UpdateCampaignAsync(id, request);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Xoá một chiến dịch tiêm chủng.
        /// </summary>
        /// <param name="id">ID chiến dịch.</param>
        /// <returns>Kết quả xoá chiến dịch.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _campaignService.DeleteCampaignAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Lấy danh sách học sinh đã tiêm trong chiến dịch.
        /// </summary>
        /// <param name="id">ID chiến dịch.</param>
        /// <returns>Danh sách học sinh đã tiêm.</returns>
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
