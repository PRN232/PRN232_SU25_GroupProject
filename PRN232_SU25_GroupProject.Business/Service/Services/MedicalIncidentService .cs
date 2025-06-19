using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<MedicalIncidentDto> CreateIncidentAsync(CreateIncidentRequest request)
        {
            var incident = _mapper.Map<MedicalIncident>(request);
            incident.ParentNotified = false;

            await _unitOfWork.MedicalIncidentRepository.AddAsync(incident);
            await _unitOfWork.SaveChangesAsync();

            // Lưu danh sách thuốc được dùng
            foreach (var medDto in request.MedicationsGiven)
            {
                var medicationGiven = new MedicationGiven
                {
                    IncidentId = incident.Id,
                    MedicationId = medDto.MedicationId,
                    Dosage = medDto.Dosage,
                    GivenAt = medDto.GivenAt
                };
                await _unitOfWork.GetRepository<MedicationGiven>().AddAsync(medicationGiven);
            }

            await _unitOfWork.SaveChangesAsync();

            return await GetIncidentByIdAsync(incident.Id);
        }

        public async Task<MedicalIncidentDto> GetIncidentByIdAsync(int id)
        {
            var incident = await _unitOfWork.MedicalIncidentRepository.GetByIdAsync(id);
            if (incident == null) return null;

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(incident.StudentId);
            var nurse = await _unitOfWork.GetRepository<SchoolNurse>().GetByIdAsync(incident.NurseId);
            var medications = await _unitOfWork.GetRepository<MedicationGiven>()
        .Query()
        .Where(m => m.IncidentId == incident.Id)
        .ToListAsync();

            var dto = _mapper.Map<MedicalIncidentDto>(incident);
            dto.StudentName = student?.FullName;
            dto.StudentCode = student?.StudentCode;
            dto.NurseName = nurse?.FullName;
            var allMedications = await _unitOfWork.MedicationRepository.GetAllAsync();


            var medsDto = _mapper.Map<List<MedicationGivenDto>>(medications);


            return dto;
        }

        public async Task<List<MedicalIncidentDto>> GetIncidentsByStudentAsync(int studentId)
        {
            var incidents = await _unitOfWork.MedicalIncidentRepository
                .Query()
                .Where(i => i.StudentId == studentId)
                .ToListAsync();

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);
            var nurseRepo = _unitOfWork.GetRepository<SchoolNurse>();

            var result = new List<MedicalIncidentDto>();
            foreach (var incident in incidents)
            {
                var nurse = await nurseRepo.GetByIdAsync(incident.NurseId);
                var dto = _mapper.Map<MedicalIncidentDto>(incident);
                dto.StudentName = student?.FullName;
                dto.StudentCode = student?.StudentCode;
                dto.NurseName = nurse?.FullName;
                result.Add(dto);
            }

            return result;
        }

        public async Task<List<MedicalIncidentDto>> GetIncidentsByDateRangeAsync(DateTime from, DateTime to)
        {
            var incidents = await _unitOfWork.MedicalIncidentRepository
                .Query()
                .Where(i => i.IncidentDate >= from && i.IncidentDate <= to)
                .ToListAsync();

            var studentRepo = _unitOfWork.StudentRepository;
            var nurseRepo = _unitOfWork.GetRepository<SchoolNurse>();

            var result = new List<MedicalIncidentDto>();

            foreach (var incident in incidents)
            {
                var student = await studentRepo.GetByIdAsync(incident.StudentId);
                var nurse = await nurseRepo.GetByIdAsync(incident.NurseId);

                var dto = _mapper.Map<MedicalIncidentDto>(incident);
                dto.StudentName = student?.FullName;
                dto.StudentCode = student?.StudentCode;
                dto.NurseName = nurse?.FullName;

                result.Add(dto);
            }

            return result;
        }

        public async Task<bool> UpdateIncidentAsync(UpdateIncidentRequest request)
        {
            var incident = await _unitOfWork.MedicalIncidentRepository.GetByIdAsync(request.Id);
            if (incident == null) return false;

            _mapper.Map(request, incident);
            _unitOfWork.MedicalIncidentRepository.Update(incident);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> NotifyParentAsync(int incidentId)
        {
            var incident = await _unitOfWork.MedicalIncidentRepository.GetByIdAsync(incidentId);
            if (incident == null) return false;

            incident.ParentNotified = true;
            _unitOfWork.MedicalIncidentRepository.Update(incident);
            await _unitOfWork.SaveChangesAsync();

            // Gửi notification ở đây (nếu có)
            return true;
        }
    }
}
