using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations
{
    public class VaccinationCampaignDto
    {
        public int Id { get; set; }
        public string CampaignName { get; set; }
        public string VaccineType { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string TargetGrades { get; set; }
        public VaccinationStatus Status { get; set; }
        public string StatusDisplay => Status.ToString();
        public int TotalStudents { get; set; }
        public int ConsentReceived { get; set; }
        public int VaccinationsCompleted { get; set; }
        public double ConsentRate => TotalStudents > 0 ? (double)ConsentReceived / TotalStudents * 100 : 0;
    }
}
