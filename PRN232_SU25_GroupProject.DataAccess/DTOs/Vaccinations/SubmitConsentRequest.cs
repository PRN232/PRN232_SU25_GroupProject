using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations
{
    public class SubmitConsentRequest
    {
        [Required]
        public int CampaignId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int ParentId { get; set; }

        [Required]
        public bool ConsentGiven { get; set; }

        public string ParentSignature { get; set; }
        public DateTime ConsentDate { get; set; } = DateTime.Now;
    }
}
