using System;
using System.Linq;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class NotificationSeeder
    {
        public static void SeedNotifications(SchoolMedicalDbContext context)
        {
            if (context.Notifications.Any()) return;

            var admin = context.Users.FirstOrDefault(u => u.UserName == "admin");
            if (admin == null) return;

            context.Notifications.Add(new Notification
            {
                UserId = admin.Id,
                Title = "Khởi tạo dữ liệu mẫu thành công",
                Message = "Hệ thống đã sẵn sàng để đăng nhập và kiểm thử.",
                CreatedAt = DateTime.Now,
                IsRead = false,
                Type = "System"
            });
            context.SaveChanges();
        }
    }
}