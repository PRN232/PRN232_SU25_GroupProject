namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.Allergy
{
    public class CreateAllergyRequest
    {
        public int MedicalProfileId { get; set; }
        public string AllergyName { get; set; }
        public string Severity { get; set; }
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
    }
}
