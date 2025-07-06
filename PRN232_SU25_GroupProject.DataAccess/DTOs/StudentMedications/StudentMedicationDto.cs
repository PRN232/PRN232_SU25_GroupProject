using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.StudentMedications
{
    public class StudentMedicationDto
    {
        [SwaggerSchemaExample("3")]
        public int Id { get; set; }
        [SwaggerSchemaExample("1")]
        public int StudentId { get; set; }
        [SwaggerSchemaExample("Rocket 1h")]
        public string MedicationName { get; set; }
        [SwaggerSchemaExample("1 viên")]
        public string Dosage { get; set; }
        [SwaggerSchemaExample("Dùng trước khi ngủ buổi tối")]
        public string Instructions { get; set; }
        [SwaggerSchemaExample("22:00:00")]
        public TimeSpan AdministrationTime { get; set; }
        [SwaggerSchemaExample("22:00")]
        public string AdministrationTimeDisplay => AdministrationTime.ToString(@"hh\:mm");
        [SwaggerSchemaExample("2025-07-06T15:52:18.960Z")]
        public DateTime StartDate { get; set; }
        [SwaggerSchemaExample("2025-11-07T15:52:18.960Z")]
        public DateTime EndDate { get; set; }
        [SwaggerSchemaExample("false")]
        public bool IsApproved { get; set; }
        [SwaggerSchemaExample("true")]
        public bool IsActive => DateTime.Now >= StartDate && DateTime.Now <= EndDate;
    }
}
