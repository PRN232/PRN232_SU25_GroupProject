using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repository.Interfaces;
using PRN232_SU25_GroupProject.DataAccess.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SchoolMedicalDbContext _context;
        private readonly Dictionary<Type, object> _repositories;
        private readonly IStudentRepository _studentRepository;

        public UnitOfWork(
    SchoolMedicalDbContext context,
    IStudentRepository studentRepository
)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
            _studentRepository = studentRepository;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repository = new GenericRepository<T>(_context);
                _repositories[typeof(T)] = repository;
            }

            return (IGenericRepository<T>)_repositories[typeof(T)];
        }

        // Expose repositories as properties (optional for convenience)
        public IGenericRepository<User> UserRepository => GetRepository<User>();

        public IGenericRepository<Parent> ParentRepository => GetRepository<Parent>();
        public IGenericRepository<MedicalProfile> MedicalProfileRepository => GetRepository<MedicalProfile>();
        public IGenericRepository<MedicalIncident> MedicalIncidentRepository => GetRepository<MedicalIncident>();
        public IGenericRepository<Medication> MedicationRepository => GetRepository<Medication>();
        public IGenericRepository<StudentMedication> StudentMedicationRepository => GetRepository<StudentMedication>();
        public IGenericRepository<VaccinationCampaign> VaccinationCampaignRepository => GetRepository<VaccinationCampaign>();
        public IGenericRepository<VaccinationRecord> VaccinationRecordRepository => GetRepository<VaccinationRecord>();
        public IGenericRepository<HealthCheckupCampaign> HealthCheckupCampaignRepository => GetRepository<HealthCheckupCampaign>();
        public IGenericRepository<HealthCheckupResult> HealthCheckupResultRepository => GetRepository<HealthCheckupResult>();
        public IGenericRepository<Notification> NotificationRepository => GetRepository<Notification>();
        public IGenericRepository<SchoolNurse> SchoolNurseRepository => GetRepository<SchoolNurse>();

        public IGenericRepository<MedicalConsent> MedicalConsentRepository => GetRepository<MedicalConsent>();
        public IStudentRepository StudentRepository => _studentRepository;
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
