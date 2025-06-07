using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IConsentFormService
    {
        Task<PagedResult<ConsentForm>> GetConsentFormsAsync(ConsentFormFilterRequest filter);
        Task<ConsentForm> GetConsentFormByIdAsync(int consentFormId);
        Task<ConsentForm> CreateConsentFormAsync(CreateConsentFormRequest request);
        Task<bool> RespondToConsentFormAsync(int consentFormId, ConsentResponse response);
        Task<IEnumerable<ConsentForm>> GetPendingConsentFormsAsync(int parentId);
        Task<IEnumerable<ConsentForm>> GetConsentFormsByStudentAsync(int studentId);
        Task<bool> SendConsentFormReminderAsync(int consentFormId);
        Task<ConsentFormStatistics> GetConsentFormStatisticsAsync(int schoolId, FormType? formType = null);
    }
}
