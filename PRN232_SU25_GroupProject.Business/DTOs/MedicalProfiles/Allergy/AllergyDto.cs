namespace PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.Allergy
{
    public class AllergyDto
    {
        public int Id { get; set; }
        public int MedicalProfileId { get; set; }
        public string AllergyName { get; set; }
        public string Severity { get; set; }
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
    }
}
