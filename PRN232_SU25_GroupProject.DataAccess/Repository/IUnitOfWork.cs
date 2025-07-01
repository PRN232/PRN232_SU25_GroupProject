using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();

        IGenericRepository<User> UserRepository { get; }

        IGenericRepository<Parent> ParentRepository { get; }
        IGenericRepository<MedicalProfile> MedicalProfileRepository { get; }
        IGenericRepository<MedicalIncident> MedicalIncidentRepository { get; }
        IGenericRepository<Medication> MedicationRepository { get; }
        IGenericRepository<StudentMedication> StudentMedicationRepository { get; }
        IGenericRepository<VaccinationCampaign> VaccinationCampaignRepository { get; }
        IGenericRepository<VaccinationRecord> VaccinationRecordRepository { get; }
        IGenericRepository<HealthCheckupCampaign> HealthCheckupCampaignRepository { get; }
        IGenericRepository<HealthCheckupResult> HealthCheckupResultRepository { get; }
        IGenericRepository<Notification> NotificationRepository { get; }
        IGenericRepository<SchoolNurse> SchoolNurseRepository { get; }
        IGenericRepository<MedicalConsent> MedicalConsentRepository { get; }
        IStudentRepository StudentRepository { get; }
        IGenericRepository<T> GetRepository<T>() where T : class;

        // Có thể thêm repository đặc biệt ở đây nếu cần
    }
}
