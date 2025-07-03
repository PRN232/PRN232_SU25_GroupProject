namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.MedicalHistory
{
    public class UpdateMedicalHistoryRequest
    {
        public int Id { get; set; }
        public int MedicalProfileId { get; set; }
        public string Condition { get; set; }
        public string Treatment { get; set; }
        public DateTime TreatmentDate { get; set; }
        public string Doctor { get; set; }
        public string Notes { get; set; }
    }
}
