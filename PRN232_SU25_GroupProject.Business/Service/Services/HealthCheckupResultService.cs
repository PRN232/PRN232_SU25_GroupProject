using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class HealthCheckupResultService : IHealthCheckupResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HealthCheckupResultService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<HealthCheckupResultDto>> RecordCheckupResultAsync(RecordCheckupRequest request)
        {
            var entity = _mapper.Map<HealthCheckupResult>(request);
            await _unitOfWork.HealthCheckupResultRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(entity.StudentId);
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(entity.CampaignId);
            var nurse = await _unitOfWork.GetRepository<SchoolNurse>().GetByIdAsync(entity.NurseId);

            var dto = _mapper.Map<HealthCheckupResultDto>(entity);
            dto.StudentName = student?.FullName;
            dto.StudentCode = student?.StudentCode;
            dto.CampaignName = campaign?.CampaignName;
            dto.NurseName = nurse?.FullName;

            return ApiResponse<HealthCheckupResultDto>.SuccessResult(dto, "Ghi nhận kết quả thành công!");
        }

        public async Task<ApiResponse<HealthCheckupResultDto>> GetResultByIdAsync(int resultId)
        {
            var result = await _unitOfWork.HealthCheckupResultRepository.GetByIdAsync(resultId);
            if (result == null)
                return ApiResponse<HealthCheckupResultDto>.ErrorResult("Không tìm thấy kết quả khám.");

            var dto = _mapper.Map<HealthCheckupResultDto>(result);
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(result.CampaignId);
            var nurse = await _unitOfWork.SchoolNurseRepository.GetByIdAsync(result.NurseId);
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(result.StudentId);
            dto.NurseName = nurse?.FullName;
            dto.StudentName = student?.FullName;
            dto.StudentCode = student?.StudentCode;
            dto.CampaignName = campaign?.CampaignName;
            return ApiResponse<HealthCheckupResultDto>.SuccessResult(dto);
        }

        public async Task<ApiResponse<List<HealthCheckupResultDto>>> GetResultsByCampaignAsync(int campaignId)
        {
            var results = await _unitOfWork.HealthCheckupResultRepository
                .Query().Where(r => r.CampaignId == campaignId).ToListAsync();

            if (results == null || !results.Any())
                return ApiResponse<List<HealthCheckupResultDto>>.ErrorResult("Không có kết quả nào.");

            // Lấy các entity liên quan
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(campaignId);
            var nurseIds = results.Select(r => r.NurseId).Distinct().ToList();
            var studentIds = results.Select(r => r.StudentId).Distinct().ToList();

            var nurses = await _unitOfWork.SchoolNurseRepository.Query()
                .Where(n => nurseIds.Contains(n.Id)).ToListAsync();
            var students = await _unitOfWork.StudentRepository.Query()
                .Where(s => studentIds.Contains(s.Id)).ToListAsync();

            var dtos = results.Select(r =>
            {
                var dto = _mapper.Map<HealthCheckupResultDto>(r);
                var nurse = nurses.FirstOrDefault(n => n.Id == r.NurseId);
                var student = students.FirstOrDefault(s => s.Id == r.StudentId);

                dto.NurseName = nurse?.FullName;
                dto.StudentName = student?.FullName;
                dto.StudentCode = student?.StudentCode;
                dto.CampaignName = campaign?.CampaignName;

                return dto;
            }).ToList();

            return ApiResponse<List<HealthCheckupResultDto>>.SuccessResult(dtos);
        }


        public async Task<ApiResponse<List<HealthCheckupResultDto>>> GetResultsByStudentAsync(int studentId, int? currentUserId, string currentUserRole)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);
            if (student == null)
                return ApiResponse<List<HealthCheckupResultDto>>.ErrorResult("Không tìm thấy học sinh này.");

            if (currentUserRole == "Parent")
            {
                if (currentUserId == null)
                    return ApiResponse<List<HealthCheckupResultDto>>.ErrorResult("Bạn không có quyền truy cập thông tin này.");
                var parent = await _unitOfWork.ParentRepository
                    .Query().FirstOrDefaultAsync(p => p.UserId == currentUserId.Value);
                if (parent == null || student.ParentId != parent.Id)
                    return ApiResponse<List<HealthCheckupResultDto>>.ErrorResult("Bạn không có quyền truy cập thông tin học sinh này.");
            }

            var results = await _unitOfWork.HealthCheckupResultRepository
                .Query().Where(r => r.StudentId == studentId).ToListAsync();

            if (results == null || !results.Any())
                return ApiResponse<List<HealthCheckupResultDto>>.ErrorResult("Học sinh chưa có lịch sử kiểm tra sức khỏe.");


            var nurseIds = results.Select(r => r.NurseId).Distinct().ToList();
            var campaignIds = results.Select(r => r.CampaignId).Distinct().ToList();
            var nurses = await _unitOfWork.SchoolNurseRepository.Query()
                .Where(n => nurseIds.Contains(n.Id)).ToListAsync();
            var campaigns = await _unitOfWork.HealthCheckupCampaignRepository.Query()
                .Where(c => campaignIds.Contains(c.Id)).ToListAsync();

            var dtos = results.Select(r =>
            {
                var dto = _mapper.Map<HealthCheckupResultDto>(r);
                var nurse = nurses.FirstOrDefault(n => n.Id == r.NurseId);
                var campaign = campaigns.FirstOrDefault(c => c.Id == r.CampaignId);

                dto.NurseName = nurse?.FullName;
                dto.StudentName = student.FullName;
                dto.StudentCode = student.StudentCode;
                dto.CampaignName = campaign?.CampaignName;

                return dto;
            }).ToList();

            return ApiResponse<List<HealthCheckupResultDto>>.SuccessResult(dtos);
        }


        public async Task<ApiResponse<HealthCheckupResultDto>> UpdateResultAsync(UpdateCheckupResultRequest request)
        {
            var result = await _unitOfWork.HealthCheckupResultRepository.GetByIdAsync(request.Id);
            if (result == null)
                return ApiResponse<HealthCheckupResultDto>.ErrorResult("Không tìm thấy kết quả khám.");
            _mapper.Map(request, result);
            _unitOfWork.HealthCheckupResultRepository.Update(result);
            await _unitOfWork.SaveChangesAsync();
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(result.CampaignId);
            var nurse = await _unitOfWork.SchoolNurseRepository.GetByIdAsync(result.NurseId);
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(result.StudentId);
            var dto = _mapper.Map<HealthCheckupResultDto>(result);
            dto.NurseName = nurse?.FullName;
            dto.StudentName = student?.FullName;
            dto.StudentCode = student?.StudentCode;
            dto.CampaignName = campaign?.CampaignName;

            return ApiResponse<HealthCheckupResultDto>.SuccessResult(_mapper.Map<HealthCheckupResultDto>(dto), "Cập nhật thành công.");
        }

        public async Task<ApiResponse<bool>> DeleteResultAsync(int resultId)
        {
            var result = await _unitOfWork.HealthCheckupResultRepository.GetByIdAsync(resultId);
            if (result == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy kết quả khám.");
            _unitOfWork.HealthCheckupResultRepository.Delete(result);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResult(true, "Xóa kết quả thành công.");
        }
    }

}
