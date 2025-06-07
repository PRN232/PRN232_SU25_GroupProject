using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface ISchoolService
    {
        Task<IEnumerable<School>> GetAllSchoolsAsync();
        Task<School> GetSchoolByIdAsync(int schoolId);
        Task<School> CreateSchoolAsync(CreateSchoolRequest request);
        Task<School> UpdateSchoolAsync(int schoolId, UpdateSchoolRequest request);
        Task<bool> DeleteSchoolAsync(int schoolId);
        Task<SchoolStatistics> GetSchoolStatisticsAsync(int schoolId);
    }
}
