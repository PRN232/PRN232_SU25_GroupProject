namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class MedicationGiven
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public int MedicationId { get; set; }
        public MedicalIncident MedicalIncident { get; set; }
        public Medication Medication { get; set; }
        public string Dosage { get; set; }
        public DateTime GivenAt { get; set; }
    }
}
