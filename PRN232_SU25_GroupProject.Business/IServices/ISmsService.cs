using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface ISmsService
    {
        Task SendSmsAsync(SmsRequest request);
        Task SendBulkSmsAsync(BulkSmsRequest request);
        Task SendEmergencyAlertAsync(EmergencyAlertRequest request);
    }

}
