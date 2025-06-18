using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        public int MedicalProfileId { get; set; }
        public string Condition { get; set; }
        public string Treatment { get; set; }
        public DateTime TreatmentDate { get; set; }
        public string Doctor { get; set; }
        public string Notes { get; set; }
    }
}
