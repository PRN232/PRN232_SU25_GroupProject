using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class VaccinationCampaign
    {
        public int Id { get; set; }
        public string CampaignName { get; set; }
        public string VaccineType { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string TargetGrades { get; set; }
        public VaccinationStatus Status { get; set; }
        public List<VaccinationConsent> Consents { get; set; }
    }
}
