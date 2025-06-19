using PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IHealthCheckupService
    {
        Task<HealthCheckupCampaignDto> CreateCampaignAsync(CreateCheckupCampaignRequest request);
        Task<bool> SendNotificationToParentsAsync(int campaignId);
        Task<List<StudentDto>> GetScheduledStudentsAsync(int campaignId);
        Task<HealthCheckupResultDto> RecordCheckupResultAsync(RecordCheckupRequest request);
        Task<List<HealthCheckupResultDto>> GetStudentCheckupHistoryAsync(int studentId);
        Task<bool> SendResultToParentAsync(int resultId);
        Task<bool> ScheduleFollowupAsync(int resultId, DateTime appointmentDate);
    }

}
