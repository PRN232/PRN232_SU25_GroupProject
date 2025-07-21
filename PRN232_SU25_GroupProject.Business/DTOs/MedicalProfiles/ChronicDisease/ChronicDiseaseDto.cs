namespace PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.ChronicDisease
{
    public class ChronicDiseaseDto
    {
        public int Id { get; set; }
        public int MedicalProfileId { get; set; }
        public string DiseaseName { get; set; }
        public string Medication { get; set; }
        public string Instructions { get; set; }
    }



}
