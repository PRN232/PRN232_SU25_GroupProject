using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.Medications;
using PRN232_SU25_GroupProject.Business.Service.IServices;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/medications")]
    [Authorize]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationService _medicationService;

        public MedicationController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        // GET: api/medications
        /// <summary>
        /// Lấy danh sách tất cả thuốc trong hệ thống.
        /// </summary>
        /// <returns>Danh sách thuốc.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicationService.GetAllMedicationsAsync();
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // GET: api/medications/{id}
        /// <summary>
        /// Lấy thông tin chi tiết của một thuốc theo ID.
        /// </summary>
        /// <param name="id">ID của thuốc.</param>
        /// <returns>Thông tin chi tiết của thuốc.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _medicationService.GetMedicationByIdAsync(id);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // POST: api/medications
        /// <summary>
        /// Thêm thuốc mới vào hệ thống.
        /// </summary>
        /// <param name="request">Thông tin thuốc cần thêm.</param>
        /// <returns>Thông tin thuốc sau khi thêm.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> Create([FromBody] AddMedicationRequest request)
        {
            var result = await _medicationService.AddMedicationAsync(request);
            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
        }

        // PUT: api/medications/{id}
        /// <summary>
        /// Cập nhật thông tin thuốc.
        /// </summary>
        /// <param name="id">ID của thuốc cần cập nhật.</param>
        /// <param name="request">Dữ liệu cập nhật.</param>
        /// <returns>Thông tin thuốc sau khi cập nhật.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMedicationRequest request)
        {
            var result = await _medicationService.UpdateMedicationAsync(id, request);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // DELETE: api/medications/{id}
        /// <summary>
        /// Xóa thuốc khỏi hệ thống.
        /// </summary>
        /// <param name="id">ID của thuốc cần xóa.</param>
        /// <returns>Kết quả xóa thuốc.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medicationService.DeleteMedicationAsync(id);
            if (!result.Success)
                return NotFound(result);

            return NoContent();
        }

    }
}
