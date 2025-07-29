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

        // Create medication for a student (Manager/Admin can create, Nurse can update)
        [HttpPost]
        [Authorize(Roles = "Admin, Manager, Parent")]  // Chỉ admin và manager có quyền tạo
        public async Task<IActionResult> Create([FromBody] CreateStudentMedicationRequest request)
        {
            var result = await _studentMedicationService.CreateStudentMedicationAsync(request);
            if (!result.Success)
                return BadRequest(result); // 400 Bad Request if error occurs

            return Ok(result); // 200 OK if successful
        }

        // Update medication for a student (Only Admin and Nurse can update)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, SchoolNurse")]  // Chỉ Admin và SchoolNurse có quyền cập nhật
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentMedicationRequest request)
        {
            var result = await _studentMedicationService.UpdateStudentMedicationAsync(id, request);
            if (!result.Success)
                return NotFound(result); // 404 Not Found if medication not found

            return Ok(result); // 200 OK if update is successful
        }

        // Get medications by student (Parent can see only their children's medications)
        [HttpGet("student/{studentId}")]
        [Authorize]  // All authenticated users can access
        public async Task<IActionResult> GetMedicationsByStudent(int studentId)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value; // Check user's role
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Parent should only be able to access medications of their children
            if (userRole == "Parent")
            {
                if (!await _studentMedicationService.CanParentAccessMedication(currentUserId, studentId))
                {
                    return Unauthorized("Bạn không có quyền truy cập thông tin này.");
                }
            }

            var result = await _studentMedicationService.GetMedicationsByStudentAsync(studentId);
            if (!result.Success)
                return NotFound(result); // 404 Not Found if no medications found

            return Ok(result); // 200 OK if medications found
        }

        // Delete medication for a student (Only Admin can delete)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]  // Only Admin can delete medications
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentMedicationService.DeleteStudentMedicationAsync(id);
            if (!result.Success)
                return NotFound(result); // 404 Not Found if medication not found

            return Ok(result); // 200 OK if successful
        }
        /// <summary>
        /// Lấy toàn bộ danh sách đơn thuốc (Admin, Manager, SchoolNurse)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentMedicationService.GetAllStudentMedicationsAsync();
            // Luôn trả về 200 OK, kể cả khi danh sách rỗng
            return Ok(result);
        }
        /// <summary>
        /// Lấy tất cả đơn thuốc của các học sinh thuộc phụ huynh (parentId)
        /// </summary>
        [HttpGet("parent/{parentId}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse, Parent")]
        public async Task<IActionResult> GetByParent(int parentId)
        {
            // Nếu role = Parent, có thể kiểm tra thêm:
            //   int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //   // compare currentUserId với parent.UserId nếu cần…

            var result = await _studentMedicationService.GetMedicationsByParentAsync(parentId);
            if (!result.Success)
                return NotFound(result);    // hoặc BadRequest tuỳ message
            return Ok(result);
        }

    }
}
