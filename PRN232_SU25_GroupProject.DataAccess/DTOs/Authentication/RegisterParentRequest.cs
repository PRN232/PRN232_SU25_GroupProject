namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication
{
    public class RegisterParentRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
