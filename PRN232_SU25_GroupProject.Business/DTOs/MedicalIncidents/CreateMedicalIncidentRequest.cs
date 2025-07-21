using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;
using System.ComponentModel.DataAnnotations;

namespace PRN232_SU25_GroupProject.Business.DTOs.MedicalIncidents
{
    public class CreateMedicalIncidentRequest
    {
        [Required]
        [SwaggerSchemaExample("2")]
        public int StudentId { get; set; }

        [Required]
        [SwaggerSchemaExample("2")]
        public int NurseId { get; set; }

        [Required]
        [SwaggerSchemaExample("Other")]
        public IncidentType Type { get; set; }

        [Required]
        [SwaggerSchemaExample("Do ăn hải sản quá nhiều")]
        public string Description { get; set; }
        [SwaggerSchemaExample("Nổi mẩn đỏ, phù mề đay")]
        public string Symptoms { get; set; }
        [SwaggerSchemaExample("Kiêng ăn các thực phẩm kẽm")]
        public string Treatment { get; set; }
        [Required]
        [SwaggerSchemaExample("High")]
        public IncidentSeverity Severity { get; set; }

        [SwaggerSchemaExample("2025-05-07T06:20:34.961Z")]
        public DateTime IncidentDate { get; set; } = DateTime.Now;
    }
}
