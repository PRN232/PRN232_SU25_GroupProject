using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalConsents
{
    public class CreateMedicalConsentRequest
    {
        public ConsentType ConsentType { get; set; }
        public int CampaignId { get; set; }
        public int StudentId { get; set; } 
    }
}
