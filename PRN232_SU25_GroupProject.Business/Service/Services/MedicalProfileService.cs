using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class MedicalProfileService : IMedicalProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MedicalProfileDto> GetProfileByStudentIdAsync(int studentId)
        {
            var profile = await _unitOfWork.MedicalProfileRepository
                .Query()
                .Include(mp => mp.Allergies)
                .Include(mp => mp.ChronicDiseases)
                .Include(mp => mp.MedicalHistories)
                .Include(mp => mp.VisionHearing)
                .FirstOrDefaultAsync(mp => mp.StudentId == studentId);

            if (profile == null) return null;

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);
            var dto = _mapper.Map<MedicalProfileDto>(profile);
            dto.StudentName = student?.FullName;

            var vaccineRecords = await _unitOfWork.VaccinationRecordRepository
                .Query()
                .Where(v => v.StudentId == studentId)
                .ToListAsync();

            dto.VaccinationRecords = _mapper.Map<List<VaccinationRecordDto>>(vaccineRecords);
            return dto;
        }

        public async Task<bool> UpdateMedicalProfileAsync(UpdateMedicalProfileRequest request)
        {
            var profile = await _unitOfWork.MedicalProfileRepository
                .Query()
                .Include(p => p.Allergies)
                .Include(p => p.ChronicDiseases)
                .Include(p => p.MedicalHistories)
                .Include(p => p.VisionHearing)
                .FirstOrDefaultAsync(p => p.StudentId == request.StudentId);

            if (profile == null) return false;

            // Clear and re-add
            profile.Allergies.Clear();
            profile.ChronicDiseases.Clear();
            profile.MedicalHistories.Clear();

            profile.Allergies = _mapper.Map<List<Allergy>>(request.Allergies);
            profile.ChronicDiseases = _mapper.Map<List<ChronicDisease>>(request.ChronicDiseases);
            profile.MedicalHistories = _mapper.Map<List<MedicalHistory>>(request.MedicalHistories);

            // Update VisionHearing
            if (profile.VisionHearing == null && request.VisionHearing != null)
            {
                profile.VisionHearing = _mapper.Map<VisionHearing>(request.VisionHearing);
            }
            else if (request.VisionHearing != null)
            {
                _mapper.Map(request.VisionHearing, profile.VisionHearing);
            }

            profile.LastUpdated = DateTime.UtcNow;

            _unitOfWork.MedicalProfileRepository.Update(profile);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddAllergyAsync(AddAllergyRequest request)
        {
            var profile = await _unitOfWork.MedicalProfileRepository
                .Query()
                .Include(p => p.Allergies)
                .FirstOrDefaultAsync(p => p.Id == request.MedicalProfileId);

            if (profile == null) return false;

            var allergy = _mapper.Map<Allergy>(request);
            profile.Allergies.Add(allergy);
            profile.LastUpdated = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddChronicDiseaseAsync(AddChronicDiseaseRequest request)
        {
            var profile = await _unitOfWork.MedicalProfileRepository
                .Query()
                .Include(p => p.ChronicDiseases)
                .FirstOrDefaultAsync(p => p.Id == request.MedicalProfileId);

            if (profile == null) return false;

            var disease = _mapper.Map<ChronicDisease>(request);
            profile.ChronicDiseases.Add(disease);
            profile.LastUpdated = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateVisionHearingAsync(UpdateVisionHearingRequest request)
        {
            var profile = await _unitOfWork.MedicalProfileRepository
                .Query()
                .Include(p => p.VisionHearing)
                .FirstOrDefaultAsync(p => p.Id == request.MedicalProfileId);

            if (profile == null) return false;

            if (profile.VisionHearing == null)
            {
                profile.VisionHearing = _mapper.Map<VisionHearing>(request);
            }
            else
            {
                _mapper.Map(request, profile.VisionHearing);
            }

            profile.LastUpdated = DateTime.UtcNow;

            _unitOfWork.MedicalProfileRepository.Update(profile);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
