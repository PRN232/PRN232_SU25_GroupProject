using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalConsents;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repository.Repositories;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class MedicalConsentService : IMedicalConsentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalConsentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET BY STUDENT
        public async Task<ApiResponse<List<MedicalConsentDto>>> GetConsentsByStudentAsync(int studentId)
        {
            var consents = await _unitOfWork.MedicalConsentRepository
                .Query()
                .Where(c => c.StudentId == studentId)
                .ToListAsync();

            // Lấy thông tin liên quan
            var campaigns = await _unitOfWork.HealthCheckupCampaignRepository.GetAllAsync();
            var students = await _unitOfWork.StudentRepository.GetAllAsync();
            var parents = await _unitOfWork.ParentRepository.GetAllAsync();

            var dtos = consents.Select(consent =>
            {
                var dto = _mapper.Map<MedicalConsentDto>(consent);

                var student = students.FirstOrDefault(s => s.Id == consent.StudentId);
                var parent = parents.FirstOrDefault(p => p.Id == student?.ParentId);
                var campaign = campaigns.FirstOrDefault(c => c.Id == consent.CampaignId);

                dto.StudentName = student?.FullName;
                dto.ParentId = parent?.Id ?? 0;
                dto.ParentName = parent?.FullName;
                dto.CampaignName = campaign?.CampaignName;

                return dto;
            }).ToList();

            return ApiResponse<List<MedicalConsentDto>>.SuccessResult(dtos);
        }

        // GET BY CAMPAIGN
        public async Task<ApiResponse<List<MedicalConsentDto>>> GetConsentsByCampaignAsync(int campaignId)
        {
            var consents = await _unitOfWork.MedicalConsentRepository
                .Query()
                .Where(c => c.CampaignId == campaignId)
                .ToListAsync();

            var campaigns = await _unitOfWork.HealthCheckupCampaignRepository.GetAllAsync();
            var students = await _unitOfWork.StudentRepository.GetAllAsync();
            var parents = await _unitOfWork.ParentRepository.GetAllAsync();

            var dtos = consents.Select(consent =>
            {
                var dto = _mapper.Map<MedicalConsentDto>(consent);

                var student = students.FirstOrDefault(s => s.Id == consent.StudentId);
                var parent = parents.FirstOrDefault(p => p.Id == student?.ParentId);
                var campaign = campaigns.FirstOrDefault(c => c.Id == consent.CampaignId);

                dto.StudentName = student?.FullName;
                dto.ParentId = parent?.Id ?? 0;
                dto.ParentName = parent?.FullName;
                dto.CampaignName = campaign?.CampaignName;

                return dto;
            }).ToList();

            return ApiResponse<List<MedicalConsentDto>>.SuccessResult(dtos);
        }


        public async Task<ApiResponse<MedicalConsentDto>> GetConsentByIdAsync(int id)
        {
            var consent = await _unitOfWork.MedicalConsentRepository.GetByIdAsync(id);
            if (consent == null)
                return ApiResponse<MedicalConsentDto>.ErrorResult("Không tìm thấy giấy đồng ý.");

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(consent.StudentId);
            var parent = student != null ? await _unitOfWork.ParentRepository.GetByIdAsync(student.ParentId) : null;
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(consent.CampaignId);

            var dto = _mapper.Map<MedicalConsentDto>(consent);
            dto.StudentName = student?.FullName;
            dto.ParentId = parent?.Id ?? 0;
            dto.ParentName = parent?.FullName;
            dto.CampaignName = campaign?.CampaignName;

            return ApiResponse<MedicalConsentDto>.SuccessResult(dto);
        }

        // CREATE (single student)
        public async Task<ApiResponse<MedicalConsentDto>> CreateMedicalConsentAsync(CreateMedicalConsentRequest request)
        {
            // 1. Truy vấn chiến dịch để lấy TargetGrades
            if (request.ConsentType != DataAccess.Enums.ConsentType.Vaccine && request.ConsentType != DataAccess.Enums.ConsentType.HealthCheckup)
                return ApiResponse<MedicalConsentDto>.ErrorResult("ConsentType không khớp (HealthCheckup/Vaccine)");

            // Declare a variable to hold the campaign
            VaccinationCampaign? vaccinationCampaign = null;
            HealthCheckupCampaign? healthCheckupCampaign = null;

            // Fetch the appropriate campaign based on ConsentType
            if (request.ConsentType == DataAccess.Enums.ConsentType.Vaccine)
            {
                vaccinationCampaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(request.CampaignId);
            }
            else if (request.ConsentType == DataAccess.Enums.ConsentType.HealthCheckup)
            {
                healthCheckupCampaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(request.CampaignId);
            }

            // Ensure that a valid campaign is found
            if (vaccinationCampaign == null && healthCheckupCampaign == null)
                return ApiResponse<MedicalConsentDto>.ErrorResult("Không tìm thấy chiến dịch.");
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
                return ApiResponse<MedicalConsentDto>.ErrorResult("Không tìm thấy học sinh.");

            var parent = await _unitOfWork.ParentRepository.GetByIdAsync(student.ParentId);
            if (parent == null)
                return ApiResponse<MedicalConsentDto>.ErrorResult("Không tìm thấy phụ huynh.");

            // Ensure the ParentId exists before inserting the consent
            var exist = await _unitOfWork.MedicalConsentRepository.Query()
                .AnyAsync(c => c.CampaignId == request.CampaignId && c.StudentId == request.StudentId && c.ConsentType == request.ConsentType);
            if (exist)
                return ApiResponse<MedicalConsentDto>.ErrorResult("Đã tồn tại giấy đồng ý cho học sinh này ở chiến dịch này.");

            var entity = _mapper.Map<MedicalConsent>(request);
            entity.ConsentDate = DateTime.UtcNow;
            entity.ParentId = student.ParentId;
            entity.ConsentGiven = false;
            entity.ParentSignature = "Điền đầy đủ họ tên phụ huynh để xác nhận";
            entity.Note = "(Nếu có)";
            await _unitOfWork.MedicalConsentRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<MedicalConsentDto>(entity);
            dto.StudentName = student?.FullName;
            dto.ParentName = parent?.FullName;
            if (request.ConsentType == DataAccess.Enums.ConsentType.HealthCheckup)
                dto.CampaignName = (await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(request.CampaignId))?.CampaignName;
            else if (request.ConsentType == DataAccess.Enums.ConsentType.Vaccine)
                dto.CampaignName = (await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(request.CampaignId))?.CampaignName;

            return ApiResponse<MedicalConsentDto>.SuccessResult(dto, "Tạo giấy đồng ý thành công.");
        }


        public async Task<ApiResponse<List<MedicalConsentDto>>> CreateMedicalConsentForClassAsync(CreateMedicalConsentClassRequest request)
        {
            // 1. Truy vấn chiến dịch để lấy TargetGrades
            if (request.ConsentType != DataAccess.Enums.ConsentType.Vaccine && request.ConsentType != DataAccess.Enums.ConsentType.HealthCheckup)
                return ApiResponse<List<MedicalConsentDto>>.ErrorResult("ConsentType không khớp (HealthCheckup/Vaccine)");

            // Declare a variable to hold the campaign
            VaccinationCampaign? vaccinationCampaign = null;
            HealthCheckupCampaign? healthCheckupCampaign = null;

            // Fetch the appropriate campaign based on ConsentType
            if (request.ConsentType == DataAccess.Enums.ConsentType.Vaccine)
            {
                vaccinationCampaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(request.CampaignId);
            }
            else if (request.ConsentType == DataAccess.Enums.ConsentType.HealthCheckup)
            {
                healthCheckupCampaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(request.CampaignId);
            }

            // Ensure that a valid campaign is found
            if (vaccinationCampaign == null && healthCheckupCampaign == null)
                return ApiResponse<List<MedicalConsentDto>>.ErrorResult("Không tìm thấy chiến dịch.");

            var targetGrades = string.Empty;
            if (vaccinationCampaign != null) targetGrades = vaccinationCampaign.TargetGrades;
            if (healthCheckupCampaign != null) targetGrades = healthCheckupCampaign.TargetGrades;

            // Split the TargetGrades and prepare for filtering
            var targetGradesList = targetGrades.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                               .Select(g => g.Trim())
                                               .ToList();

            // Query students based on targetGrades
            var students = await _unitOfWork.StudentRepository.Query()
                .Where(s => targetGradesList.Contains(s.ClassName))
                .ToListAsync();

            if (!students.Any())
                return ApiResponse<List<MedicalConsentDto>>.ErrorResult("Không có học sinh nào trong các lớp mục tiêu.");

            var consents = new List<MedicalConsent>();
            var alreadyCreatedConsents = new List<string>(); // To track students who already have consents
            foreach (var student in students)
            {
                // Ensure the ParentId exists before querying
                if (student.ParentId == 0)
                {
                    Console.WriteLine($"Học sinh {student.FullName} không có phụ huynh.");
                    return ApiResponse<List<MedicalConsentDto>>.ErrorResult($"Học sinh {student.FullName} không có phụ huynh.");
                }

                var parent = await _unitOfWork.ParentRepository.GetByIdAsync(student.ParentId);
                if (parent == null)
                {
                    Console.WriteLine($"Phụ huynh của học sinh {student.FullName} không tồn tại.");
                    return ApiResponse<List<MedicalConsentDto>>.ErrorResult($"Phụ huynh của học sinh {student.FullName} không tồn tại.");
                }

                // Check if the consent already exists
                var exist = await _unitOfWork.MedicalConsentRepository.Query()
                    .AnyAsync(c => c.CampaignId == request.CampaignId && c.StudentId == student.Id && c.ConsentType == request.ConsentType);

                if (!exist)
                {
                    var entity = new MedicalConsent
                    {
                        ConsentType = request.ConsentType,
                        CampaignId = request.CampaignId,
                        StudentId = student.Id,
                        ParentId = student.ParentId,
                        ConsentGiven = false,
                        ParentSignature = "Điền đầy đủ họ tên phụ huynh để xác nhận",
                        Note = "(Nếu có)",
                        ConsentDate = DateTime.UtcNow
                    };
                    consents.Add(entity);
                    await _unitOfWork.MedicalConsentRepository.AddAsync(entity);
                }
                else
                {
                    alreadyCreatedConsents.Add(student.FullName); // Track the students with already created consents
                }
            }

            await _unitOfWork.SaveChangesAsync();

            // Ensure the number of consents created is correct
            if (consents.Count == 0)
            {
                return ApiResponse<List<MedicalConsentDto>>.ErrorResult("Không có giấy đồng ý nào được tạo do đã được tạo từ trước");
            }

            // Map to DTO and return
            var dtoList = new List<MedicalConsentDto>();

            foreach (var consent in consents)
            {
                var dto = _mapper.Map<MedicalConsentDto>(consent);
                var student = await _unitOfWork.StudentRepository.GetByIdAsync(consent.StudentId);
                var parent = student != null ? await _unitOfWork.ParentRepository.GetByIdAsync(student.ParentId) : null;

                dto.StudentName = student?.FullName ?? string.Empty;
                dto.ParentName = parent?.FullName ?? string.Empty;
                dto.CampaignName = vaccinationCampaign?.CampaignName ?? healthCheckupCampaign?.CampaignName ?? string.Empty;
                dtoList.Add(dto);
            }

            string message = $"Đã tạo {dtoList.Count} giấy đồng ý cho các học sinh.";
            if (alreadyCreatedConsents.Any())
            {
                message += $" Các học sinh sau đã có giấy đồng ý trước đó: {string.Join(", ", alreadyCreatedConsents)}";
            }

            return ApiResponse<List<MedicalConsentDto>>.SuccessResult(dtoList, message);
        }







        // UPDATE
        public async Task<ApiResponse<MedicalConsentDto>> UpdateMedicalConsentAsync(int id, UpdateMedicalConsentRequest request, int currentUserId)
        {
            var consent = await _unitOfWork.MedicalConsentRepository.GetByIdAsync(id);
            if (consent == null)
                return ApiResponse<MedicalConsentDto>.ErrorResult("Không tìm thấy giấy đồng ý.");

            // Fetch the student linked to this consent to get the ParentId
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(consent.StudentId);
            if (student == null)
                return ApiResponse<MedicalConsentDto>.ErrorResult("Không tìm thấy học sinh liên quan.");
            var parrentid = await _unitOfWork.ParentRepository.FindAsync(p => p.Id == student.ParentId);
            // Check if the current user is the parent of the student
            if (student.ParentId != parrentid.UserId)
                return ApiResponse<MedicalConsentDto>.ErrorResult("Bạn không có quyền sửa đổi giấy đồng ý này.");

            // Update consent details
            consent.ConsentGiven = request.ConsentGiven;
            consent.ParentSignature = request.ParentSignature;
            consent.Note = request.Note;
            consent.ConsentDate = DateTime.UtcNow;

            _unitOfWork.MedicalConsentRepository.Update(consent);
            await _unitOfWork.SaveChangesAsync();

            // Retrieve the parent and campaign details
            var parent = await _unitOfWork.ParentRepository.GetByIdAsync(student.ParentId);
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(consent.CampaignId);

            // Map to DTO
            var dto = _mapper.Map<MedicalConsentDto>(consent);
            dto.StudentName = student?.FullName;
            dto.ParentId = parent?.Id ?? 0;
            dto.ParentName = parent?.FullName;
            dto.CampaignName = campaign?.CampaignName;

            return ApiResponse<MedicalConsentDto>.SuccessResult(dto, "Cập nhật giấy đồng ý thành công.");
        }


        // DELETE
        public async Task<ApiResponse<bool>> DeleteMedicalConsentAsync(int id)
        {
            var consent = await _unitOfWork.MedicalConsentRepository.GetByIdAsync(id);
            if (consent == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy giấy đồng ý.");

            _unitOfWork.MedicalConsentRepository.Delete(consent);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResult(true, "Xóa giấy đồng ý thành công.");
        }
    }
}
