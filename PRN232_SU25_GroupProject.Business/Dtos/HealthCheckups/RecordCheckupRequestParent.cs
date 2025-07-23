using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Dtos.HealthCheckups
{
    public class RecordCheckupRequestParent
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public decimal Height { get; set; }

        [Required]
        public decimal Weight { get; set; }

        public string BloodPressure { get; set; }
        public string VisionTest { get; set; }
        public string HearingTest { get; set; }
        public string GeneralHealth { get; set; }
        public bool RequiresFollowup { get; set; }
        public string Recommendations { get; set; }
        public DateTime CheckupDate { get; set; } = DateTime.Now;
    }
}
