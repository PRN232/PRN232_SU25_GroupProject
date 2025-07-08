using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;
using System.ComponentModel.DataAnnotations;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents
{
    public class UpdateMedicalIncidentRequest
    {

        [SwaggerSchemaExample("Dị ứng lần 2 do ăn hải sản")]
        public string Description { get; set; }
        [SwaggerSchemaExample("Nổi mẩn đỏ, da thô ráp, ngứa phần da bị đỏ")]
        public string Symptoms { get; set; }
        [SwaggerSchemaExample("Quản lý chặt chẽ phần ăn, không cho ăn đồ ăn có kẽm")]
        public string Treatment { get; set; }
        [SwaggerSchemaExample("Critical")]
        public IncidentSeverity Severity { get; set; }
        [SwaggerSchemaExample("true")]
        public bool ParentNotified { get; set; }
    }
}
