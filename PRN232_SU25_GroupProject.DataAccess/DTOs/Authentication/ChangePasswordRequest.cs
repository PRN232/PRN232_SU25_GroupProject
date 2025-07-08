using System.ComponentModel.DataAnnotations;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication
{
    public class ChangePasswordRequest
    {
        public int UserId { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
