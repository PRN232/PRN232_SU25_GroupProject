using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.Students;
using PRN232_SU25_GroupProject.Business.Service.IServices;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    /// <summary>
    /// Quản lý thông tin học sinh.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Tạo mới một học sinh.
        /// </summary>
        /// <param name="request">Thông tin học sinh cần tạo.</param>
        /// <returns>Kết quả tạo học sinh.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest request)
        {
            var response = await _studentService.CreateStudentAsync(request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin học sinh theo ID.
        /// </summary>
        /// <param name="id">ID học sinh.</param>
        /// <returns>Thông tin học sinh.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var response = await _studentService.GetStudentByIdAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Trả về danh sách học sinh thuộc một lớp cụ thể.
        /// </summary>
        /// <param name="className">Tên lớp học.</param>
        /// <returns>Danh sách học sinh trong lớp.</returns>
        [HttpGet("classes/{className}/students")]
        public async Task<IActionResult> GetStudentsInClass(string className)
        {
            var response = await _studentService.GetStudentsByClassAsync(className);
            return Ok(response);
        }

        /// <summary>
        /// Trả về danh sách học sinh thuộc một phụ huynh.
        /// </summary>
        /// <param name="parentId">ID phụ huynh.</param>
        /// <returns>Danh sách học sinh của phụ huynh.</returns>
        [HttpGet("parent/{parentId}")]
        public async Task<IActionResult> GetStudentsByParent(int parentId)
        {
            var response = await _studentService.GetStudentsByParentAsync(parentId);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật thông tin học sinh.
        /// </summary>
        /// <param name="id">ID học sinh.</param>
        /// <param name="request">Thông tin cần cập nhật.</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentRequest request)
        {
            var response = await _studentService.UpdateStudentAsync(id, request);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách tất cả học sinh.
        /// </summary>
        /// <returns>Danh sách học sinh.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var response = await _studentService.GetAllStudentsAsync();
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Trả về danh sách các lớp và số lượng học sinh trong từng lớp.
        /// </summary>
        /// <returns>Danh sách lớp và số lượng học sinh.</returns>
        [HttpGet("classes")]
        public async Task<IActionResult> GetClassSummaries()
        {
            var response = await _studentService.GetClassSummariesAsync();
            return Ok(response);
        }
    }
}
