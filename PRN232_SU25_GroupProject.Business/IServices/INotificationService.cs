using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface INotificationService
    {
        Task<PagedResult<Notification>> GetNotificationsAsync(int userId, NotificationFilterRequest filter);
        Task<Notification> CreateNotificationAsync(CreateNotificationRequest request);
        Task<bool> MarkAsReadAsync(int notificationId);
        Task<bool> MarkAllAsReadAsync(int userId);
        Task<bool> DeleteNotificationAsync(int notificationId);
        Task<int> GetUnreadCountAsync(int userId);
        Task SendBulkNotificationAsync(BulkNotificationRequest request);
        Task SendPushNotificationAsync(int userId, PushNotificationRequest request);
    }
}
