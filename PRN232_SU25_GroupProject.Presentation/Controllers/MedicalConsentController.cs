﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.DTOs.MedicalConsents;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using System.Security.Claims;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{

    [ApiController]
    [Route("api/medical-consents")]
    public class MedicalConsentController : ControllerBase
    {
        private readonly IMedicalConsentService _medicalConsentService;

        public MedicalConsentController(IMedicalConsentService medicalConsentService)
        {
            _medicalConsentService = medicalConsentService;
        }

        /// <summary>
        /// Lấy danh sách giấy đồng ý của học sinh.
        /// </summary>
        /// <remarks>
        /// **Sample Request:**  
        /// GET /api/medical-consents/student/11
        /// </remarks>
        /// <param name="studentId">ID học sinh</param>
        /// <returns>
        /// <para>Sample Response:</para>
        /// <code>
        /// {
        ///   "success": true,
        ///   "data": [
        ///     {
        ///       "id": 9,
        ///       "consentType": "HealthCheckup",
        ///       "campaignId": 3,
        ///       "studentId": 11,
        ///       "consentGiven": true,
        ///       "parentSignature": "Nguyễn Văn A",
        ///       "note": null,
        ///       "consentDate": "2025-07-10T12:00:00"
        ///     }
        ///   ],
        ///   "message": "Success",
        ///   "errors": []
        /// }
        /// </code>
        /// </returns>
        [HttpGet("student/{studentId}")]
        [Authorize]
        public async Task<IActionResult> GetConsentsByStudent(int studentId)
        {
            var res = await _medicalConsentService.GetConsentsByStudentAsync(studentId);
            return Ok(res);
        }

        /// <summary>
        /// Lấy danh sách giấy đồng ý theo campaign (chiến dịch)
        /// </summary>
        /// <remarks>
        /// **Sample Request:**  
        /// GET /api/medical-consents/campaign/3
        /// </remarks>
        [HttpGet("campaign/{campaignId}")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse,Parent")]
        public async Task<IActionResult> GetConsentsByCampaign(int campaignId)
        {
            var res = await _medicalConsentService.GetConsentsByCampaignAsync(campaignId);
            return Ok(res);
        }

        /// <summary>
        /// Lấy chi tiết 1 giấy đồng ý
        /// </summary>
        /// <remarks>
        /// **Sample Request:**  
        /// GET /api/medical-consents/5
        /// </remarks>
        /// <returns>
        /// <para>Sample Response:</para>
        /// <code>
        /// {
        ///   "success": true,
        ///   "data": {
        ///     "id": 5,
        ///     "consentType": "Vaccination",
        ///     "campaignId": 2,
        ///     "studentId": 7,
        ///     "consentGiven": false,
        ///     "parentSignature": null,
        ///     "note": "Chưa đồng ý",
        ///     "consentDate": "2025-07-01T00:00:00"
        ///   },
        ///   "message": "Success",
        ///   "errors": []
        /// }
        /// </code>
        /// </returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse,Parent")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _medicalConsentService.GetConsentByIdAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Tạo giấy đồng ý cho 1 học sinh (Manager,Admin gửi)
        /// </summary>
        /// <remarks>
        /// **Sample Request:**  
        /// POST /api/medical-consents
        /// <br />
        /// <code>
        /// {
        ///   consentType": "Vaccine" hoặc "HealthCheckup",
        ///   "campaignId": 1,
        ///   "studentId": 3
        /// }
        /// </code>
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "Manager,Admin,SchoolNurse,Parent")]
        public async Task<IActionResult> Create([FromBody] CreateMedicalConsentRequest request)
        {
            var res = await _medicalConsentService.CreateMedicalConsentAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        /// <summary>
        /// Tạo giấy đồng ý cho nhiều học sinh trong các lớp (Manager,Admin gửi)
        /// </summary>
        /// <remarks>
        /// **Sample Request:**  
        /// POST /api/medical-consents/class
        /// <br />
        /// <code>
        /// {
        ///   consentType": "Vaccine" hoặc "HealthCheckup",
        ///   "campaignId": 1
        /// }
        /// </code>
        /// </remarks>
        [HttpPost("class")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse,Parent")]
        public async Task<IActionResult> CreateForClass([FromBody] CreateMedicalConsentClassRequest request)
        {
            var res = await _medicalConsentService.CreateMedicalConsentForClassAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }

        /// <summary>
        /// Cập nhật giấy đồng ý (Parent sửa lại)
        /// </summary>
        /// <remarks>
        /// **Sample Request:**  
        /// PUT /api/medical-consents/12
        /// <br />
        /// <code>
        /// {
        ///   "consentGiven": true,
        ///   "parentSignature": "Đặng Quang Huy",
        ///   "note": "Đồng ý tiêm chủng"
        /// }
        /// </code>
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize(Roles = "Parent")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMedicalConsentRequest request)
        {
            var parentIdClaim = User.FindFirst("ParentId");

            if (parentIdClaim == null || !int.TryParse(parentIdClaim.Value, out int parentId))
            {
                return Forbid("Không xác định được danh tính phụ huynh. Token thiếu hoặc sai ParentId.");
            }

            var res = await _medicalConsentService.UpdateMedicalConsentAsync(id, request, parentId);
            if (!res.Success)
                return NotFound(res);

            return Ok(res);
        }


        /// <summary>
        /// Xóa giấy đồng ý (Manager/Admin)
        /// </summary>
        /// <remarks>
        /// **Sample Request:**  
        /// DELETE /api/medical-consents/15
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse,Parent")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _medicalConsentService.DeleteMedicalConsentAsync(id);
            if (!res.Success) return NotFound(res);
            return Ok(res);
        }

        /// <summary>
        /// Lấy trạng thái consent của tất cả học sinh trong các lớp thuộc TargetGrades của chiến dịch.
        /// </summary>
        /// <param name="campaignId">ID chiến dịch</param>
        /// <param name="consentType">Loại consent (Vaccine | HealthCheckup)</param>
        /// <returns>Danh sách StudentConsentStatusDto</returns>
        [HttpGet("campaign/{campaignId}/students/status")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse,Parent")]
        public async Task<IActionResult> GetConsentStatusByCampaign(
            int campaignId,
            [FromQuery] ConsentType consentType)
        {
            var res = await _medicalConsentService
                .GetConsentStatusByCampaignAsync(campaignId, consentType);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        /// <summary>
        /// Lấy trạng thái consent của học sinh trong 1 lớp cụ thể thuộc TargetGrades của chiến dịch.
        /// </summary>
        /// <param name="campaignId">ID chiến dịch</param>
        /// <param name="className">Tên lớp</param>
        /// <param name="consentType">Loại consent (Vaccine | HealthCheckup)</param>
        /// <returns>Danh sách StudentConsentStatusDto</returns>
        [HttpGet("campaign/{campaignId}/classes/{className}/students/status")]
        [Authorize(Roles = "Manager,Admin,SchoolNurse,Parent")]
        public async Task<IActionResult> GetConsentStatusByCampaignAndClass(
            int campaignId,
            string className,
            [FromQuery] ConsentType consentType)
        {
            var res = await _medicalConsentService
                .GetConsentStatusByCampaignAndClassAsync(campaignId, consentType, className);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
    }

}

//repush