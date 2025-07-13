namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Students
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string StudentCode { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
        public string Gender { get; set; }
        public string ClassName { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public string ParentPhone { get; set; }
        public bool HasMedicalProfile { get; set; }
    }
}
