using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations
{
    public class CreateVaccinationCampaignRequest
    {
        [Required]
        public string CampaignName { get; set; }

        [Required]
        public string VaccineType { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public string TargetGrades { get; set; }

        public string Description { get; set; }
    }

}
