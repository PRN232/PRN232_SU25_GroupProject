using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Medications;

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
