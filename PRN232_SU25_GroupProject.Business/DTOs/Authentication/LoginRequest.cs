using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
namespace PRN232_SU25_GroupProject.Business.DTOs.Authentication
{
    [SwaggerSchema(Required = new[] { "Description" })]
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        [SwaggerSchemaExample("admin@school.vn")]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [SwaggerSchemaExample("String_1")]
        public string Password { get; set; }
        [SwaggerSchemaExample("true")]
        public bool RememberMe { get; set; }
    }

}