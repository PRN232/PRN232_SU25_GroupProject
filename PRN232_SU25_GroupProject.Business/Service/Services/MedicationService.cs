using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Medications;
using PRN232_SU25_GroupProject.DataAccess.Repository.Repositories;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MedicationDto>> GetAllMedicationsAsync()
        {
            var medications = await _unitOfWork.MedicationRepository.GetAllAsync();
            return _mapper.Map<List<MedicationDto>>(medications);
        }

        public async Task<MedicationDto> GetMedicationByIdAsync(int id)
        {
            var medication = await _unitOfWork.MedicationRepository.GetByIdAsync(id);
            return medication != null ? _mapper.Map<MedicationDto>(medication) : null;
        }

        public async Task<bool> UpdateStockAsync(int medicationId, int quantity)
        {
            var medication = await _unitOfWork.MedicationRepository.GetByIdAsync(medicationId);
            if (medication == null) return false;

            medication.StockQuantity = quantity;
            _unitOfWork.MedicationRepository.Update(medication);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<List<MedicationDto>> GetExpiringMedicationsAsync(DateTime beforeDate)
        {
            var medications = await _unitOfWork.MedicationRepository
                .Query()
                .Where(m => m.ExpiryDate <= beforeDate)
                .ToListAsync();

            return _mapper.Map<List<MedicationDto>>(medications);
        }
    }
}
