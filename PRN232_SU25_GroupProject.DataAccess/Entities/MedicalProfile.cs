namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class MedicalProfile
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public List<Allergy> Allergies { get; set; }
        public List<ChronicDisease> ChronicDiseases { get; set; }
        public List<MedicalHistory> MedicalHistories { get; set; }
        public List<VaccinationRecord> VaccinationRecords { get; set; }
        public DateTime LastUpdated { get; set; }
    }

}
