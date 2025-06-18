using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles
{
    public class VisionHearingDto
    {
        public int Id { get; set; }
        public string VisionLeft { get; set; }
        public string VisionRight { get; set; }
        public string HearingLeft { get; set; }
        public string HearingRight { get; set; }
        public DateTime LastChecked { get; set; }
    }
}
