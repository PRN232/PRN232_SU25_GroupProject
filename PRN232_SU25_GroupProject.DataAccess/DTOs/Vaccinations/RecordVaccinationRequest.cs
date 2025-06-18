using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations
{
    public class RecordVaccinationRequest
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CampaignId { get; set; }

        [Required]
        public string VaccineType { get; set; }

        [Required]
        public string BatchNumber { get; set; }

        [Required]
        public int NurseId { get; set; }

        public string SideEffects { get; set; }

        [Required]
        public VaccinationResult Result { get; set; }

        public DateTime VaccinationDate { get; set; } = DateTime.Now;
    }
}
