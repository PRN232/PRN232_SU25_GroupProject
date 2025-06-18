using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class MedicalProfile
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public List<Allergy> Allergies { get; set; }
        public List<ChronicDisease> ChronicDiseases { get; set; }
        public List<MedicalHistory> MedicalHistories { get; set; }
        public VisionHearing VisionHearing { get; set; }
        public List<VaccinationRecord> VaccinationRecords { get; set; }
        public DateTime LastUpdated { get; set; }
    }

}
