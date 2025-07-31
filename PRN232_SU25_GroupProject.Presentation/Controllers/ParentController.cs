using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.Parents;
using PRN232_SU25_GroupProject.Business.Service.IServices;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    /// <summary>
    /// Quản lý thông tin phụ huynh học sinh.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        /// <summary>
        /// Lấy thông tin phụ huynh dựa trên ID người dùng (userId).
        /// </summary>
        /// <param name="userId">ID của người dùng liên kết với phụ huynh.</param>
        /// <returns>Thông tin phụ huynh.</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetParentByUserId(int userId)
        {
            var response = await _parentService.GetParentByUserIdAsync(userId);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách con của phụ huynh theo ID.
        /// </summary>
        /// <param name="parentId">ID của phụ huynh.</param>
        /// <returns>Danh sách học sinh là con của phụ huynh.</returns>
        [HttpGet("{parentId}/children")]
        public async Task<IActionResult> GetChildren(int parentId)
        {
            var response = await _parentService.GetChildrenAsync(parentId);
            return Ok(response);
        }

        /// <summary>
        /// Cập nhật thông tin phụ huynh.
        /// </summary>
        /// <param name="userId">ID người dùng liên kết với phụ huynh.</param>
        /// <param name="request">Thông tin cần cập nhật.</param>
        /// <returns>Kết quả cập nhật thông tin phụ huynh.</returns>
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateParentInfo(int userId, [FromBody] UpdateParentRequest request)
        {
            var response = await _parentService.UpdateParentInfoAsync(userId, request);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
    }
}
