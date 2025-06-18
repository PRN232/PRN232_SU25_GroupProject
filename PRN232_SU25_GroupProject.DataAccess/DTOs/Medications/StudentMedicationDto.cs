using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Medications
{
    public class StudentMedicationDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public TimeSpan AdministrationTime { get; set; }
        public string AdministrationTimeDisplay => AdministrationTime.ToString(@"hh\:mm");
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive => DateTime.Now >= StartDate && DateTime.Now <= EndDate;
    }
}
