using PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IHealthCheckupService
    {
        Task<HealthCheckupCampaign> CreateCampaignAsync(CreateCheckupCampaignRequest request);
        Task<bool> SendNotificationToParentsAsync(int campaignId);
        Task<List<Student>> GetScheduledStudentsAsync(int campaignId);
        Task<HealthCheckupResult> RecordCheckupResultAsync(RecordCheckupRequest request);
        Task<List<HealthCheckupResult>> GetStudentCheckupHistoryAsync(int studentId);
        Task<bool> SendResultToParentAsync(int resultId);
        Task<bool> ScheduleFollowupAsync(int resultId, DateTime appointmentDate);
    }
}
