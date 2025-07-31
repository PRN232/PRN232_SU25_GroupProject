using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.StudentMedications;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using System.Security.Claims;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/student-medications")]
    [Authorize] // Chỉ những người đã đăng nhập mới có quyền truy cập vào các API này
    public class StudentMedicationController : ControllerBase
    {
        private readonly IStudentMedicationService _studentMedicationService;

        public StudentMedicationController(IStudentMedicationService studentMedicationService)
        {
            _studentMedicationService = studentMedicationService;
        }

        /// <summary>
        /// Tạo đơn thuốc mới cho học sinh. (Chỉ Admin, Manager và Phụ huynh được phép tạo)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin, Manager, Parent")]
        public async Task<IActionResult> Create([FromBody] CreateStudentMedicationRequest request)
        {
            var result = await _studentMedicationService.CreateStudentMedicationAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        /// Cập nhật đơn thuốc của học sinh. (Chỉ Admin và y tá trường có quyền cập nhật)
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, SchoolNurse")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentMedicationRequest request)
        {
            var result = await _studentMedicationService.UpdateStudentMedicationAsync(id, request);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách đơn thuốc theo mã học sinh. (Phụ huynh chỉ được xem đơn thuốc của con mình)
        /// </summary>
        [HttpGet("student/{studentId}")]
        [Authorize]
        public async Task<IActionResult> GetMedicationsByStudent(int studentId)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (userRole == "Parent")
            {
                if (!await _studentMedicationService.CanParentAccessMedication(currentUserId, studentId))
                {
                    return Unauthorized("Bạn không có quyền truy cập thông tin này.");
                }
            }

            var result = await _studentMedicationService.GetMedicationsByStudentAsync(studentId);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// Xóa đơn thuốc theo mã. (Chỉ Admin được phép xóa)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentMedicationService.DeleteStudentMedicationAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        /// <summary>
        /// Lấy toàn bộ danh sách đơn thuốc. (Chỉ Admin, Manager, SchoolNurse)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentMedicationService.GetAllStudentMedicationsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lấy tất cả đơn thuốc của các học sinh có phụ huynh là người dùng chỉ định. (Admin, Manager, Nurse và chính phụ huynh)
        /// </summary>
        [HttpGet("parent/{parentId}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse, Parent")]
        public async Task<IActionResult> GetByParent(int parentId)
        {
            var result = await _studentMedicationService.GetMedicationsByParentAsync(parentId);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
    }
}
