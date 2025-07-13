using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicationGivens
{
    public class UpdateMedicationsGivenRequest
    {
        [SwaggerSchemaExample("2")]
        public int IncidentId { get; set; }
        [SwaggerSchemaExample("2")]
        public int MedicationId { get; set; }
        [SwaggerSchemaExample("Ngày 2 viên, sáng tối")]
        public string Dosage { get; set; }
        [SwaggerSchemaExample("2025-05-11T06:20:34.961Z")]
        public DateTime GiveAt { get; set; }
        
    }
}
