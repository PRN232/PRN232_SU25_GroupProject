using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolMedicalDbContext _context;

        public IRepository<Student> Students { get; }
        public IRepository<Parent> Parents { get; }
        public IRepository<MedicalProfile> MedicalProfiles { get; }
        public IRepository<Medication> Medications { get; }
        public IRepository<MedicalIncident> MedicalIncidents { get; }
        public IRepository<VaccinationCampaign> VaccinationCampaigns { get; }

        public UnitOfWork(SchoolMedicalDbContext context)
        {
            _context = context;

            Students = new Repository<Student>(_context);
            Parents = new Repository<Parent>(_context);
            MedicalProfiles = new Repository<MedicalProfile>(_context);
            Medications = new Repository<Medication>(_context);
            MedicalIncidents = new Repository<MedicalIncident>(_context);
            VaccinationCampaigns = new Repository<VaccinationCampaign>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
