using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IMedicalEventService
    {
        Task<PagedResult<MedicalEvent>> GetMedicalEventsAsync(MedicalEventFilterRequest filter);
        Task<MedicalEvent> GetMedicalEventByIdAsync(int eventId);
        Task<MedicalEvent> CreateMedicalEventAsync(CreateMedicalEventRequest request);
        Task<MedicalEvent> UpdateMedicalEventAsync(int eventId, UpdateMedicalEventRequest request);
        Task<bool> DeleteMedicalEventAsync(int eventId);
        Task<IEnumerable<MedicalEvent>> GetMedicalEventsByStudentAsync(int studentId);
        Task<bool> NotifyParentAsync(int eventId);
        Task<MedicalEventStatistics> GetMedicalEventStatisticsAsync(int schoolId, DateTime? from = null, DateTime? to = null);
    }
}
