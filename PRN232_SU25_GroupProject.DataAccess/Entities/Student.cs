namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentCode { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ClassName { get; set; }
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public MedicalProfile MedicalProfile { get; set; }
    }
}
