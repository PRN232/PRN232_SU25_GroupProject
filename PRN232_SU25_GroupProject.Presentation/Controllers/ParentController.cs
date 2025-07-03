using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Parents;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;
        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetParentByUserId(int userId)
        {
            var response = await _parentService.GetParentByUserIdAsync(userId);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        [HttpGet("{parentId}/children")]
        public async Task<IActionResult> GetChildren(int parentId)
        {
            var response = await _parentService.GetChildrenAsync(parentId);
            return Ok(response);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateParentInfo(int userId, [FromBody] UpdateParentRequest request)
        {
            var response = await _parentService.UpdateParentInfoAsync(userId, request);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
    }

}
