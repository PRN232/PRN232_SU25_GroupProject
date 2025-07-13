namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.ChronicDisease
{
    public class CreateChronicDiseaseRequest
    {
        public int MedicalProfileId { get; set; }
        public string DiseaseName { get; set; }
        public string Medication { get; set; }
        public string Instructions { get; set; }
    }
}
