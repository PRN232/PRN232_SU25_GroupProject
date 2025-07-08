namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalConsents
{
    public class UpdateMedicalConsentRequest
    {
        public bool ConsentGiven { get; set; }
        public string ParentSignature { get; set; }
        public string Note { get; set; }
    }
}
