using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class MedicalIncident
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int NurseId { get; set; }
        public SchoolNurse Nurse { get; set; }
        public IncidentType Type { get; set; }
        public string Description { get; set; }
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
        public IncidentSeverity Severity { get; set; }
        public bool ParentNotified { get; set; }
        public DateTime IncidentDate { get; set; }
        public List<MedicationGiven> MedicationsGiven { get; set; }
    }
}
