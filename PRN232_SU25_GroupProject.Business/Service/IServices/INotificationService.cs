using PRN232_SU25_GroupProject.Business.DTOs.Notifications;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface INotificationService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body);
        Task<bool> SendSMSAsync(string phoneNumber, string message);
        Task<bool> CreateInAppNotificationAsync(int userId, string message, string title = "Thông báo", string type = "Info");
        Task<List<NotificationDto>> GetUserNotificationsAsync(int userId);
    }
}
