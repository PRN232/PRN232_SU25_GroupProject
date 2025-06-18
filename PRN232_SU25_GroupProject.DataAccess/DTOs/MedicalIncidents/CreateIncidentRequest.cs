using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents
{
    public class CreateIncidentRequest
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int NurseId { get; set; }

        [Required]
        public IncidentType Type { get; set; }

        [Required]
        public string Description { get; set; }

        public string Symptoms { get; set; }
        public string Treatment { get; set; }

        [Required]
        public IncidentSeverity Severity { get; set; }

        public DateTime IncidentDate { get; set; } = DateTime.Now;
        public List<MedicationGivenDto> MedicationsGiven { get; set; } = new List<MedicationGivenDto>();
    }
}
