using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class VaccinationConsent
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int StudentId { get; set; }
        public int ParentId { get; set; }
        public bool ConsentGiven { get; set; }
        public DateTime ConsentDate { get; set; }
        public string ParentSignature { get; set; }
    }
}
