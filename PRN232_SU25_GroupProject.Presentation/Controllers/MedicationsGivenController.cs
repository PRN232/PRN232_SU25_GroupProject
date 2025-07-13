using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicationGivens;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/medications-given")]
    public class MedicationsGivenController : ControllerBase
    {
        private readonly IMedicationsGivenService _medicationsGivenService;

        public MedicationsGivenController(IMedicationsGivenService medicationsGivenService)
        {
            _medicationsGivenService = medicationsGivenService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager, SchoolNurse, Parent")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicationsGivenService.GetAllMedicationsGivenAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse, Parent")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _medicationsGivenService.GetMedicationsGivenByIdAsync(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager, SchoolNurse, Parent")]
        public async Task<IActionResult> Create([FromBody] CreateMedicationsGivenRequest request)
        {
            var result = await _medicationsGivenService.CreateMedicationsGivenAsync(request);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse, Parent")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMedicationsGivenRequest request)
        {
            var result = await _medicationsGivenService.UpdateMedicationsGivenAsync(id, request);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

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
