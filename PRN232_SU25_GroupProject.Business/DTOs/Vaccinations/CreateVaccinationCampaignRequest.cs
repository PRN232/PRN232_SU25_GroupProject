using System.ComponentModel.DataAnnotations;

namespace PRN232_SU25_GroupProject.Business.DTOs.Vaccinations
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


    }

}
