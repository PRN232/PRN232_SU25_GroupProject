using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication
{
    [SwaggerSchema(Required = new[] { "Description" })]
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        [SwaggerSchema("admin@school.vn")]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [SwaggerSchema("String_1")]
        public string Password { get; set; }
        [SwaggerSchema("true")]
        public bool RememberMe { get; set; }
    }

}