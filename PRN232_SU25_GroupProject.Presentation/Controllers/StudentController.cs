using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.Students;
using PRN232_SU25_GroupProject.Business.Service.IServices;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest request)
        {
            var response = await _studentService.CreateStudentAsync(request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var response = await _studentService.GetStudentByIdAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Trả về tất cả học sinh trong lớp
        /// </summary>
        [HttpGet("classes/{className}/students")]
        public async Task<IActionResult> GetStudentsInClass(string className)
        {
            var response = await _studentService.GetStudentsByClassAsync(className);
            return Ok(response);
        }

        [HttpGet("parent/{parentId}")]
        public async Task<IActionResult> GetStudentsByParent(int parentId)
        {
            var response = await _studentService.GetStudentsByParentAsync(parentId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentRequest request)
        {
            var response = await _studentService.UpdateStudentAsync(id, request);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var response = await _studentService.GetAllStudentsAsync();
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
        /// <summary>
        /// Trả về danh sách các lớp và số lượng học sinh trong từng lớp
        /// </summary>
        [HttpGet("classes")]
        public async Task<IActionResult> GetClassSummaries()
        {
            var response = await _studentService.GetClassSummariesAsync();
            return Ok(response);
        }
    }

}
