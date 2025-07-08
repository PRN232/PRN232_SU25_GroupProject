namespace PRN232_SU25_GroupProject.DataAccess.Entities
{
    public class Parent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<Student> Children { get; set; }
    }
}
