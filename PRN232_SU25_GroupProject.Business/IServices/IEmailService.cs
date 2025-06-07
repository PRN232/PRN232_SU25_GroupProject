using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest request);
        Task SendBulkEmailAsync(BulkEmailRequest request);
        Task SendHealthReportEmailAsync(int studentId, string reportData);
        Task SendVaccinationReminderEmailAsync(int parentId, VaccinationReminderData data);
        Task SendConsentFormEmailAsync(int parentId, ConsentFormEmailData data);
        Task SendMedicalEventNotificationEmailAsync(int parentId, MedicalEventNotificationData data);
    }

}
