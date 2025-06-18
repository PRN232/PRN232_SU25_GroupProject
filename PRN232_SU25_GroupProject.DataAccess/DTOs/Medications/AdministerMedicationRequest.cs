using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Medications
{
    public class AdministerMedicationRequest
    {
        public int StudentMedicationId { get; set; }
        public int NurseId { get; set; }
        public DateTime AdministeredAt { get; set; } = DateTime.Now;
        public string Notes { get; set; }
    }
}
