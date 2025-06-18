using PRN232_SU25_GroupProject.DataAccess.DTOs;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Reports;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IReportService
    {
        Task<DashboardData> GetDashboardDataAsync(int? userId = null);
        Task<List<IncidentReport>> GetIncidentReportsAsync(DateTime from, DateTime to);
        Task<List<VaccinationReport>> GetVaccinationReportsAsync(int campaignId);
        Task<List<HealthCheckupReport>> GetHealthCheckupReportsAsync(int campaignId);
        Task<StudentHealthSummary> GetStudentHealthSummaryAsync(int studentId);
    }
}
