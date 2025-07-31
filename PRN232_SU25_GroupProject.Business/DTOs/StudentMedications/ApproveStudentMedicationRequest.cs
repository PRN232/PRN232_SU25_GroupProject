using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;

namespace PRN232_SU25_GroupProject.Business.DTOs.StudentMedications
{
    public class ApproveStudentMedicationRequest
    {
        [SwaggerSchemaExample("3")]
        public int MedicationId { get; set; }
        [SwaggerSchemaExample("true")]
        public MedicationApprovalStatus IsApproved { get; set; }
    }
}
