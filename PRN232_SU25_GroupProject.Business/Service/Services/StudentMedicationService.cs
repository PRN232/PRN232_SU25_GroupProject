using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Medications;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class StudentMedicationService : IStudentMedicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentMedicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<StudentMedicationDto>> GetStudentMedicationsAsync(int studentId)
        {
            var meds = await _unitOfWork.StudentMedicationRepository
                .Query()
                .Where(m => m.StudentId == studentId && m.IsApproved)
                .ToListAsync();

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);
            var parent = await _unitOfWork.GetRepository<Parent>()
                .GetByIdAsync(student?.ParentId ?? 0);

            return meds.Select(m =>
            {
                var dto = _mapper.Map<StudentMedicationDto>(m);
                dto.StudentName = student?.FullName;
                dto.StudentCode = student?.StudentCode;
                dto.ParentName = parent?.FullName;
                return dto;
            }).ToList();
        }

        public async Task<StudentMedicationDto> SubmitMedicationRequestAsync(SubmitMedicationRequest request)
        {
            var entity = _mapper.Map<StudentMedication>(request);
            entity.IsApproved = false;

            await _unitOfWork.StudentMedicationRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            // Map sang DTO để trả về đúng kiểu
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(entity.StudentId);
            var parent = await _unitOfWork.GetRepository<Parent>().GetByIdAsync(entity.ParentId);

            var dto = _mapper.Map<StudentMedicationDto>(entity);
            dto.StudentName = student?.FullName;
            dto.StudentCode = student?.StudentCode;
            dto.ParentName = parent?.FullName;

            return dto;
        }


        public async Task<List<StudentMedicationDto>> GetPendingApprovalsAsync()
        {
            var meds = await _unitOfWork.StudentMedicationRepository
                .Query()
                .Where(m => !m.IsApproved)
                .ToListAsync();

            // Lấy thông tin Student/Parent nếu cần
            var studentIds = meds.Select(m => m.StudentId).Distinct().ToList();
            var parentIds = meds.Select(m => m.ParentId).Distinct().ToList();

            var students = await _unitOfWork.StudentRepository.Query()
                .Where(s => studentIds.Contains(s.Id))
                .ToDictionaryAsync(s => s.Id);

            var parents = await _unitOfWork.GetRepository<Parent>().Query()
                .Where(p => parentIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            var dtoList = meds.Select(m =>
            {
                var dto = _mapper.Map<StudentMedicationDto>(m);

                if (students.TryGetValue(m.StudentId, out var student))
                {
                    dto.StudentName = student.FullName;
                    dto.StudentCode = student.StudentCode;
                }

                if (parents.TryGetValue(m.ParentId, out var parent))
                {
                    dto.ParentName = parent.FullName;
                }

                return dto;
            }).ToList();

            return dtoList;
        }


        public async Task<bool> ApproveMedicationAsync(int requestId, int nurseId)
        {
            var medication = await _unitOfWork.StudentMedicationRepository.GetByIdAsync(requestId);
            if (medication == null || medication.IsApproved)
                return false;

            medication.IsApproved = true;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        

        public async Task<bool> AdministerMedicationAsync(AdministerMedicationRequest request)
        {
            var studentMedication = await _unitOfWork.StudentMedicationRepository
                .GetByIdAsync(request.StudentMedicationId);

            if (studentMedication == null || !studentMedication.IsApproved)
                return false;

            var medicationGiven = new MedicationGiven
            {
                IncidentId = 0, // Optional: if not related to incident
                MedicationId = 0, // Not used in StudentMedication — only name exists
                Dosage = studentMedication.Dosage,
                GivenAt = request.AdministeredAt
            };

            await _unitOfWork.GetRepository<MedicationGiven>().AddAsync(medicationGiven);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
