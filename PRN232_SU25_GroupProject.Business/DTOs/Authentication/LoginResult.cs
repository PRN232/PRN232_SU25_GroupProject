namespace PRN232_SU25_GroupProject.Business.DTOs.Authentication
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
