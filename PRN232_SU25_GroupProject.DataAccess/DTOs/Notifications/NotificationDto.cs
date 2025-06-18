using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Notifications
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }
        public string TimeAgo
        {
            get
            {
                var timeSpan = DateTime.Now - CreatedAt;
                if (timeSpan.Days > 0) return $"{timeSpan.Days} days ago";
                if (timeSpan.Hours > 0) return $"{timeSpan.Hours} hours ago";
                if (timeSpan.Minutes > 0) return $"{timeSpan.Minutes} minutes ago";
                return "Just now";
            }
        }
    }
}
