using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class Vaccination
    {
        [Key]
        public int VaccinationID { get; set; }

        [Required]
        public int CampaignID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        [MaxLength(100)]
        public string VaccineName { get; set; }

        [Required]
        public DateTime VaccinationDate { get; set; }

        [MaxLength(50)]
        public string BatchNumber { get; set; }

        [MaxLength(50)]
        public string Dosage { get; set; }

        [Required]
        public int AdministeredBy { get; set; }

        [MaxLength(500)]
        public string Reaction { get; set; }

        public DateTime? NextVaccinationDate { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [ForeignKey("CampaignID")]
        public virtual VaccinationCampaign Campaign { get; set; }

        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("AdministeredBy")]
        public virtual User AdministeredByUser { get; set; }
    }
}
