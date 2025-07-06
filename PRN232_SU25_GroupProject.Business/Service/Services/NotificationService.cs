using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Notifications;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            // Giả lập gửi email – bạn có thể tích hợp SMTP, SendGrid, v.v.
            Console.WriteLine($"[Email] To: {to} | Subject: {subject} | Body: {body}");
            return await Task.FromResult(true);
        }

        public async Task<bool> SendSMSAsync(string phoneNumber, string message)
        {
            // Giả lập gửi SMS – bạn có thể tích hợp Twilio, ViettelSMS, v.v.
            Console.WriteLine($"[SMS] To: {phoneNumber} | Message: {message}");
            return await Task.FromResult(true);
        }

        public async Task<bool> CreateInAppNotificationAsync(int userId, string message, string title = "Thông báo", string type = "Info")
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.Now,
                Type = type
            };

            await _unitOfWork.NotificationRepository.AddAsync(notification);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<NotificationDto>> GetUserNotificationsAsync(int userId)
        {
            var notifications = await _unitOfWork.NotificationRepository
                .Query()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return _mapper.Map<List<NotificationDto>>(notifications);
        }
    }
}
