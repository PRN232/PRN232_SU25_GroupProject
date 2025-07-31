using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.MedicalIncidents;
using PRN232_SU25_GroupProject.Business.Service.IServices;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/medical-incidents")]
    public class MedicalIncidentController : ControllerBase
    {
        private readonly IMedicalIncidentService _medicalIncidentService;

        public MedicalIncidentController(IMedicalIncidentService medicalIncidentService)
        {
            _medicalIncidentService = medicalIncidentService;
        }

        /// <summary>
        /// Lấy danh sách tất cả sự cố y tế.
        /// </summary>
        /// <returns>Danh sách các sự cố y tế.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicalIncidentService.GetAllIncidentsAsync();
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// Lấy thông tin chi tiết một sự cố y tế theo ID.
        /// </summary>
        /// <param name="id">ID sự cố y tế.</param>
        /// <returns>Thông tin chi tiết sự cố y tế.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _medicalIncidentService.GetIncidentByIdAsync(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// Tạo mới một sự cố y tế.
        /// </summary>
        /// <param name="request">Thông tin sự cố y tế cần tạo.</param>
        /// <returns>Sự cố y tế vừa được tạo.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> Create([FromBody] CreateMedicalIncidentRequest request)
        {
            var result = await _medicalIncidentService.CreateIncidentAsync(request);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
        }

        /// <summary>
        /// Cập nhật thông tin sự cố y tế.
        /// </summary>
        /// <param name="id">ID sự cố cần cập nhật.</param>
        /// <param name="request">Thông tin cập nhật.</param>
        /// <returns>Sự cố y tế sau khi cập nhật.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMedicalIncidentRequest request)
        {
            var result = await _medicalIncidentService.UpdateIncidentAsync(id, request);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// Xóa một sự cố y tế.
        /// </summary>
        /// <param name="id">ID sự cố cần xóa.</param>
        /// <returns>Kết quả xóa sự cố.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medicalIncidentService.DeleteIncidentAsync(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }
    }
}
