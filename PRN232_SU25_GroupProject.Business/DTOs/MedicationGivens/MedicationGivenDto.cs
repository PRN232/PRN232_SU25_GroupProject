namespace PRN232_SU25_GroupProject.Business.DTOs.MedicationGivens
{
    public class MedicationGivenDto
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public int MedicationId { get; set; }
        public DateTime GiveAt { get; set; }
        public string Dosage { get; set; }
    }
}
