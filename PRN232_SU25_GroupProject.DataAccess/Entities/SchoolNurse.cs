namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class SchoolNurse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string FullName { get; set; }
        public string LicenseNumber { get; set; }
        public string Department { get; set; }
    }
}
