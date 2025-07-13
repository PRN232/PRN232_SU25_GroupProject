using PRN232_SU25_GroupProject.DataAccess.DTOs.Reports;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
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
