using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;

namespace PRN232_SU25_GroupProject.Business.DTOs.Medications
{
    public class UpdateMedicationRequest
    {
        [SwaggerSchemaExample("Rocket 24h")]
        public string Name { get; set; }
        [SwaggerSchemaExample("Enhance Stamina")]
        public string Type { get; set; }
        [SwaggerSchemaExample("Tăng cường sinh lực")]
        public string Description { get; set; }
        [SwaggerSchemaExample("16")]
        public int StockQuantity { get; set; }
        [SwaggerSchemaExample("2726-07-06T23:42:58.9380195")]
        public DateTime ExpiryDate { get; set; }
        [SwaggerSchemaExample("Tránh tiếp xúc với không khí")]
        public string StorageInstructions { get; set; }
    }
}
