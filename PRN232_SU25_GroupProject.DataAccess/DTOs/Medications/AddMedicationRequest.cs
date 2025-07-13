using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Medications
{
    public class AddMedicationRequest
    {
        [SwaggerSchemaExample("Rocket 12h")]
        public string Name { get; set; }
        [SwaggerSchemaExample("Enhance Stamina")]
        public string Type { get; set; }
        [SwaggerSchemaExample("Tăng cường sinh lực")]
        public string Description { get; set; }
        [SwaggerSchemaExample("9")]
        public int StockQuantity { get; set; }
        [SwaggerSchemaExample("3026-07-06T23:42:58.9380195")]
        public DateTime ExpiryDate { get; set; }
        [SwaggerSchemaExample("Tránh tiếp xúc với không khí")]
        public string StorageInstructions { get; set; }
    }
}
