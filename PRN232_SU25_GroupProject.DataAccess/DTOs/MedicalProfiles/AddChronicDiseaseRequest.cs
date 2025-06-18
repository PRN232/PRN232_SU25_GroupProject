using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles
{
    public class AddChronicDiseaseRequest
    {
        public int MedicalProfileId { get; set; }

        [Required]
        public string DiseaseName { get; set; }

        public string Medication { get; set; }
        public string Instructions { get; set; }
    }
}
