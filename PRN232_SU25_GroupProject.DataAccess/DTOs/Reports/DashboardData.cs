using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Reports
{
    public class DashboardData
    {
        public int TotalStudents { get; set; }
        public int TotalIncidentsToday { get; set; }
        public int TotalIncidentsThisWeek { get; set; }
        public int PendingMedicationApprovals { get; set; }
        public int ActiveVaccinationCampaigns { get; set; }
        public int ActiveCheckupCampaigns { get; set; }
        public int LowStockMedications { get; set; }
        public int ExpiringMedications { get; set; }
        public List<RecentIncident> RecentIncidents { get; set; } = new List<RecentIncident>();
        public List<UpcomingEvent> UpcomingEvents { get; set; } = new List<UpcomingEvent>();
    }
}
