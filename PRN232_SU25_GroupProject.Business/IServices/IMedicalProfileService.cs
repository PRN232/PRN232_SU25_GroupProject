using PRN232_SU25_GroupProject.DataAccess.DTOs;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IMedicalProfileService
    {
        Task<MedicalProfile> GetProfileByStudentIdAsync(int studentId);
        Task<bool> UpdateMedicalProfileAsync(UpdateMedicalProfileRequest request);
        Task<bool> AddAllergyAsync(AddAllergyRequest request);
        Task<bool> AddChronicDiseaseAsync(AddChronicDiseaseRequest request);
        Task<bool> UpdateVisionHearingAsync(UpdateVisionHearingRequest request);
    }
}
