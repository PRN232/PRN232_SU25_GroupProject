using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations
{
    public class VaccinationRecordDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string VaccineType { get; set; }
        public DateTime VaccinationDate { get; set; }
        public string BatchNumber { get; set; }
        public int NurseId { get; set; }
        public string NurseName { get; set; }
        public string SideEffects { get; set; }
        public VaccinationResult Result { get; set; }
        public string ResultDisplay => Result.ToString();
    }
}
