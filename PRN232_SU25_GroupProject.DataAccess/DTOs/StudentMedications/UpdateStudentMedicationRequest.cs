using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.StudentMedications
{
    public class UpdateStudentMedicationRequest
    {
        [SwaggerSchemaExample("3")]
        public int Id { get; set; } // ID của medication cần cập nhật
        [SwaggerSchemaExample("1")]
        public int StudentId { get; set; } // ID của học sinh
        [SwaggerSchemaExample("Rocket 12h")]
        public string MedicationName { get; set; } // Tên thuốc
        [SwaggerSchemaExample("1 viên")]
        public string Dosage { get; set; } // Liều lượng
        [SwaggerSchemaExample("Dùng trước khi ngủ buổi tối\"")]
        public string Instructions { get; set; } // Hướng dẫn sử dụng
        [SwaggerSchemaExample("22:00:00")]
        public TimeSpan AdministrationTime { get; set; } // Thời gian uống thuốc
        [SwaggerSchemaExample("2025-07-06T15:52:18.960Z")]
        public DateTime StartDate { get; set; } // Ngày bắt đầu sử dụng thuốc
        [SwaggerSchemaExample("2025-11-07T15:52:18.960Z")]
        public DateTime EndDate { get; set; } // Ngày kết thúc sử dụng thuốc
        [SwaggerSchemaExample("true")]
        public bool IsApproved { get; set; } // Tình trạng duyệt thuốc (Phê duyệt bởi y tá)

    }
}
