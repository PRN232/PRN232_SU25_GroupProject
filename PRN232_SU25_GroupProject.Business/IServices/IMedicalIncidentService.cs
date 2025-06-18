using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IMedicalIncidentService
    {
        Task<MedicalIncident> CreateIncidentAsync(CreateIncidentRequest request);
        Task<MedicalIncident> GetIncidentByIdAsync(int id);
        Task<List<MedicalIncident>> GetIncidentsByStudentAsync(int studentId);
        Task<List<MedicalIncident>> GetIncidentsByDateRangeAsync(DateTime from, DateTime to);
        Task<bool> UpdateIncidentAsync(UpdateIncidentRequest request);
        Task<bool> NotifyParentAsync(int incidentId);
    }
}
