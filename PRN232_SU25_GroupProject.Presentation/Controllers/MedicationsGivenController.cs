using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.MedicationGivens;
using PRN232_SU25_GroupProject.Business.Service.IServices;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    /// <summary>
    /// Quản lý thông tin thuốc đã được cấp/phát cho học sinh.
    /// </summary>
    [ApiController]
    [Route("api/medications-given")]
    public class MedicationsGivenController : ControllerBase
    {
        private readonly IMedicationsGivenService _medicationsGivenService;

        public MedicationsGivenController(IMedicationsGivenService medicationsGivenService)
        {
            _medicationsGivenService = medicationsGivenService;
        }

        /// <summary>
        /// Lấy danh sách tất cả bản ghi thuốc đã được cấp.
        /// </summary>
        /// <returns>Danh sách thuốc đã cấp.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin, Manager, SchoolNurse, Parent")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicationsGivenService.GetAllMedicationsGivenAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lấy chi tiết bản ghi thuốc đã cấp theo ID.
        /// </summary>
        /// <param name="id">ID của bản ghi thuốc đã cấp.</param>
        /// <returns>Chi tiết bản ghi thuốc đã cấp.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse, Parent")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _medicationsGivenService.GetMedicationsGivenByIdAsync(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// Tạo mới bản ghi thuốc đã được cấp cho học sinh.
        /// </summary>
        /// <param name="request">Thông tin bản ghi cần tạo.</param>
        /// <returns>Kết quả tạo bản ghi thuốc.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin, Manager, SchoolNurse, Parent")]
        public async Task<IActionResult> Create([FromBody] CreateMedicationsGivenRequest request)
        {
            var result = await _medicationsGivenService.CreateMedicationsGivenAsync(request);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
        }

        /// <summary>
        /// Cập nhật thông tin bản ghi thuốc đã được cấp.
        /// </summary>
        /// <param name="id">ID bản ghi thuốc đã cấp.</param>
        /// <param name="request">Thông tin cập nhật.</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse, Parent")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMedicationsGivenRequest request)
        {
            var result = await _medicationsGivenService.UpdateMedicationsGivenAsync(id, request);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// Xoá một bản ghi thuốc đã cấp.
        /// </summary>
        /// <param name="id">ID bản ghi cần xoá.</param>
        /// <returns>Kết quả xoá bản ghi.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse, Parent")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medicationsGivenService.DeleteMedicationsGivenAsync(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }
    }
}
