using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class HealthCheckupResult
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CampaignId { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodPressure { get; set; }
        public string VisionTest { get; set; }
        public string HearingTest { get; set; }
        public string GeneralHealth { get; set; }
        public bool RequiresFollowup { get; set; }
        public string Recommendations { get; set; }
        public DateTime CheckupDate { get; set; }
        public int NurseId { get; set; }
    }
}
