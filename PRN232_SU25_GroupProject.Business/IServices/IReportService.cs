using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IReportService
    {
        Task<HealthReportData> GenerateHealthReportAsync(HealthReportRequest request);
        Task<VaccinationReportData> GenerateVaccinationReportAsync(VaccinationReportRequest request);
        Task<MedicalEventReportData> GenerateMedicalEventReportAsync(MedicalEventReportRequest request);
        Task<StudentHealthSummaryReport> GenerateStudentHealthSummaryAsync(int studentId);
        Task<SchoolHealthOverviewReport> GenerateSchoolHealthOverviewAsync(int schoolId, int year);
        Task<byte[]> ExportReportToPdfAsync(ReportType reportType, object reportData);
        Task<byte[]> ExportReportToExcelAsync(ReportType reportType, object reportData);
    }

}
