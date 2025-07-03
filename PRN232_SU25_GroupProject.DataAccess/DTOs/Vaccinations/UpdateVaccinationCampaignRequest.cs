using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations
{
    public class UpdateVaccinationCampaignRequest
    {

        public string CampaignName { get; set; }

        public string VaccineType { get; set; }


        public DateTime ScheduledDate { get; set; }

        public string TargetGrades { get; set; }
        public VaccinationStatus Status { get; set; }

    }
}
