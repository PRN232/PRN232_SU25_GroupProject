using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles
{
    public class ChronicDiseaseDto
    {
        public int Id { get; set; }
        public string DiseaseName { get; set; }
        public string Medication { get; set; }
        public string Instructions { get; set; }
    }
}
