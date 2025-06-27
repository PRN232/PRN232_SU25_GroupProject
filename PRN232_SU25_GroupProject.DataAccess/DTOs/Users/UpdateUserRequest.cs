using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Users
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
