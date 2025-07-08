using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents;

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

        [HttpGet]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicalIncidentService.GetAllIncidentsAsync();
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _medicalIncidentService.GetIncidentByIdAsync(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> Create([FromBody] CreateMedicalIncidentRequest request)
        {
            var result = await _medicalIncidentService.CreateIncidentAsync(request);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager, SchoolNurse")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMedicalIncidentRequest request)
        {
            var result = await _medicalIncidentService.UpdateIncidentAsync(id, request);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

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
