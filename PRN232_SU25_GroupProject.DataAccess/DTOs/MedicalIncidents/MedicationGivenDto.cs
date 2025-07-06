namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents
{
    public class MedicationGivenDto
    {
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public DateTime GivenAt { get; set; }
    }
}
