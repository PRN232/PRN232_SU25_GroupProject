using System.ComponentModel.DataAnnotations;

namespace PRN232_SU25_GroupProject.Business.DTOs.Users
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
    }
}
