using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IHealthRecordService
    {
        Task<HealthRecord> GetHealthRecordByStudentIdAsync(int studentId);
        Task<HealthRecord> CreateHealthRecordAsync(CreateHealthRecordRequest request);
        Task<HealthRecord> UpdateHealthRecordAsync(int studentId, UpdateHealthRecordRequest request);
        Task<bool> DeleteHealthRecordAsync(int healthRecordId);
        Task<IEnumerable<HealthRecord>> GetHealthRecordsBySchoolAsync(int schoolId);
        Task<HealthRecordStatistics> GetHealthRecordStatisticsAsync(int schoolId);
    }
}
