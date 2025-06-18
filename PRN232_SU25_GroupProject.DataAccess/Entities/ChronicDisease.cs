using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class ChronicDisease
    {
        public int Id { get; set; }
        public int MedicalProfileId { get; set; }
        public string DiseaseName { get; set; }
        public string Medication { get; set; }
        public string Instructions { get; set; }
    }
}
