using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicationGivens;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class MedicalIncidentService : IMedicalIncidentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalIncidentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<MedicalIncidentDto>>> GetAllIncidentsAsync()
        {
            var incidents = await _unitOfWork.MedicalIncidentRepository.GetAllAsync();
            if (incidents == null || !incidents.Any())
            {
                return ApiResponse<List<MedicalIncidentDto>>.ErrorResult("Không có MedialIncidents nào tồn tại");
            }
            var dtos = new List<MedicalIncidentDto>();
            var students = await _unitOfWork.StudentRepository.GetAllAsync();
            var nurses = await _unitOfWork.SchoolNurseRepository.GetAllAsync();
            foreach (var incident in incidents)
            {
                if (incident == null)
                {
                    // Skip if the incident is null, you can log it if needed
                    continue;
                }

                var dto = _mapper.Map<MedicalIncidentDto>(incident);

                // Ensure the MedicalIncidentId is valid before querying
                if (incident.Id != 0)
                {
                    var medicationsGiven = await _unitOfWork.MedicationGivenRepository.Query()
                        .Where(mg => mg.IncidentId == incident.Id)
                        .ToListAsync();

                    // If medicationsGiven is null or empty, return an empty list for MedicationsGiven
                    dto.MedicationsGiven = medicationsGiven == null ? new List<MedicationGivenDto>() : _mapper.Map<List<MedicationGivenDto>>(medicationsGiven);
                    dto.StudentCode = students.FirstOrDefault(s => s.Id == incident.StudentId)?.StudentCode;
                    dto.StudentName = students.FirstOrDefault(s => s.Id == incident.StudentId)?.FullName;
                    dto.NurseName = nurses.FirstOrDefault(n => n.Id == incident.NurseId)?.FullName;
                }

                dtos.Add(dto);
            }

            return ApiResponse<List<MedicalIncidentDto>>.SuccessResult(dtos);
        }


        public async Task<ApiResponse<MedicalIncidentDto>> GetIncidentByIdAsync(int id)
        {
            var incident = await _unitOfWork.MedicalIncidentRepository.GetByIdAsync(id);
            if (incident == null)
                return ApiResponse<MedicalIncidentDto>.ErrorResult("Không tìm thấy sự kiện y tế.");
            var students = await _unitOfWork.StudentRepository.GetAllAsync();
            var nurses = await _unitOfWork.SchoolNurseRepository.GetAllAsync();
            var dto = _mapper.Map<MedicalIncidentDto>(incident);
            dto.StudentCode = students.FirstOrDefault(s => s.Id == incident.StudentId)?.StudentCode;
            dto.StudentName = students.FirstOrDefault(s => s.Id == incident.StudentId)?.FullName;
            dto.NurseName = nurses.FirstOrDefault(n => n.Id == incident.NurseId)?.FullName;

            return ApiResponse<MedicalIncidentDto>.SuccessResult(dto);
        }

        public async Task<ApiResponse<MedicalIncidentDto>> CreateIncidentAsync(CreateMedicalIncidentRequest request)
        {
            var entity = _mapper.Map<MedicalIncident>(request);
            entity.IncidentDate = DateTime.UtcNow;
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(entity.StudentId);
            if (student == null)
                return ApiResponse<MedicalIncidentDto>.ErrorResult("Không tìm thấy học sinh liên quan đến sự kiện y tế.");
            var nurse = await _unitOfWork.SchoolNurseRepository.GetByIdAsync(entity.NurseId);
            if (nurse == null)
                return ApiResponse<MedicalIncidentDto>.ErrorResult("Không tìm thấy y tá liên quan đến sự kiện y tế.");

            await _unitOfWork.MedicalIncidentRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            var students = await _unitOfWork.StudentRepository.GetAllAsync();
            var nurses = await _unitOfWork.SchoolNurseRepository.GetAllAsync();
            var dto = _mapper.Map<MedicalIncidentDto>(entity);
            dto.StudentCode = students.FirstOrDefault(s => s.Id == entity.StudentId)?.StudentCode;
            dto.StudentName = students.FirstOrDefault(s => s.Id == entity.StudentId)?.FullName;
            dto.NurseName = nurses.FirstOrDefault(n => n.Id == entity.NurseId)?.FullName;
            return ApiResponse<MedicalIncidentDto>.SuccessResult(dto, "Tạo sự kiện y tế thành công.");
        }

        public async Task<ApiResponse<MedicalIncidentDto>> UpdateIncidentAsync(int id, UpdateMedicalIncidentRequest request)
        {
            var incident = await _unitOfWork.MedicalIncidentRepository.GetByIdAsync(id);
            if (incident == null)
                return ApiResponse<MedicalIncidentDto>.ErrorResult("Không tìm thấy sự kiện y tế.");
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(incident.StudentId);
            if (student == null)
                return ApiResponse<MedicalIncidentDto>.ErrorResult("Không tìm thấy học sinh liên quan đến sự kiện y tế.");
            var nurse = await _unitOfWork.SchoolNurseRepository.GetByIdAsync(incident.NurseId);
            if (nurse == null)
                return ApiResponse<MedicalIncidentDto>.ErrorResult("Không tìm thấy y tá liên quan đến sự kiện y tế.");

            _mapper.Map(request, incident);
            _unitOfWork.MedicalIncidentRepository.Update(incident);
            await _unitOfWork.SaveChangesAsync();
            var students = await _unitOfWork.StudentRepository.GetAllAsync();
            var nurses = await _unitOfWork.SchoolNurseRepository.GetAllAsync();
            var dto = _mapper.Map<MedicalIncidentDto>(incident);
            dto.StudentCode = students.FirstOrDefault(s => s.Id == incident.StudentId)?.StudentCode;
            dto.StudentName = students.FirstOrDefault(s => s.Id == incident.StudentId)?.FullName;
            dto.NurseName = nurses.FirstOrDefault(n => n.Id == incident.NurseId)?.FullName;
            return ApiResponse<MedicalIncidentDto>.SuccessResult(dto, "Cập nhật sự kiện y tế thành công.");
        }

        public async Task<ApiResponse<bool>> DeleteIncidentAsync(int id)
        {
            var incident = await _unitOfWork.MedicalIncidentRepository.GetByIdAsync(id);
            if (incident == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy sự kiện y tế.");

            _unitOfWork.MedicalIncidentRepository.Delete(incident);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResult(true, "Xóa sự kiện y tế thành công.");
        }
    }
}