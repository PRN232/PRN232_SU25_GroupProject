﻿using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.Allergy;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IAllergyService
    {
        Task<ApiResponse<List<AllergyDto>>> GetAllergiesByProfileAsync(int medicalProfileId);
        Task<ApiResponse<AllergyDto>> GetAllergyByIdAsync(int allergyId);
        Task<ApiResponse<AllergyDto>> CreateAllergyAsync(CreateAllergyRequest request);
        Task<ApiResponse<AllergyDto>> UpdateAllergyAsync(UpdateAllergyRequest request);
        Task<ApiResponse<bool>> DeleteAllergyAsync(int allergyId);
    }
}
