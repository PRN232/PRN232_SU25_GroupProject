using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;

namespace PRN232_SU25_GroupProject.Business.DTOs.MedicationGivens
{
    public class CreateMedicationsGivenRequest
    {
        [SwaggerSchemaExample("2")]
        public int IncidentId { get; set; }
        [SwaggerSchemaExample("2")]
        public int MedicationId { get; set; }
        [SwaggerSchemaExample("Ngày 3 viên, mỗi viên sau mỗi bữa ăn")]
        public string Dosage { get; set; }

    }
}
