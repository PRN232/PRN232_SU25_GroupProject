using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalConsents
{
    public class MedicalConsentDto
    {
        public int Id { get; set; }
        public ConsentType ConsentType { get; set; }
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public bool ConsentGiven { get; set; }
        public DateTime ConsentDate { get; set; }
        public string ParentSignature { get; set; }
        public string Note { get; set; }
    }
}
