using PRN232_SU25_GroupProject.DataAccess.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public UserDto User { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
