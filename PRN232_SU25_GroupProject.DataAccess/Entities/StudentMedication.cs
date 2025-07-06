namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class StudentMedication
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public TimeSpan AdministrationTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ParentId { get; set; }
        public bool IsApproved { get; set; }
    }
}
