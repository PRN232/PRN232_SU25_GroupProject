using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Students { get; }
        IRepository<Parent> Parents { get; }
        IRepository<MedicalProfile> MedicalProfiles { get; }
        IRepository<Medication> Medications { get; }
        IRepository<MedicalIncident> MedicalIncidents { get; }
        IRepository<VaccinationCampaign> VaccinationCampaigns { get; }
        // ... thêm các repository cần dùng

        Task<int> CompleteAsync();
    }
}
