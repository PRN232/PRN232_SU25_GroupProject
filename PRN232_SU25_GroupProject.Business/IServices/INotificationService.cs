using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface INotificationService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body);
        Task<bool> SendSMSAsync(string phoneNumber, string message);
        Task<bool> CreateInAppNotificationAsync(int userId, string message);
        Task<List<Notification>> GetUserNotificationsAsync(int userId);
    }
}
