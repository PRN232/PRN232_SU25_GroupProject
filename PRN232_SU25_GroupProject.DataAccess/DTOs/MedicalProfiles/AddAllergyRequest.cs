using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles
{
    public class AddAllergyRequest
    {
        public int MedicalProfileId { get; set; }

        [Required]
        public string AllergyName { get; set; }

        [Required]
        public string Severity { get; set; }

        public string Symptoms { get; set; }
        public string Treatment { get; set; }
    }
}
