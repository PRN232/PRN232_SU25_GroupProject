using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.StudentMedications
{
    public class ApproveStudentMedicationRequest
    {
        [SwaggerSchemaExample("3")]
        public int MedicationId { get; set; }
        [SwaggerSchemaExample("true")]
        public bool IsApproved { get; set; }
    }
}
