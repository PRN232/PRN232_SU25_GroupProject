using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Medications
{
    public class MedicationDto
    {
        [SwaggerSchemaExample("1")]
        public int Id { get; set; }
        [SwaggerSchemaExample("Paracetamol 500mg")]
        public string Name { get; set; }
        [SwaggerSchemaExample("PainRelief")]
        public string Type { get; set; }
        [SwaggerSchemaExample("Giảm đau, hạ sốt")]
        public string Description { get; set; }
        [SwaggerSchemaExample("300")]
        public int StockQuantity { get; set; }
        [SwaggerSchemaExample("2027-07-06T23:42:58.9380195")]
        public DateTime ExpiryDate { get; set; }
        [SwaggerSchemaExample("Nơi khô ráo")]
        public string StorageInstructions { get; set; }

        public bool IsExpired => ExpiryDate < DateTime.Now;
        public bool IsLowStock => StockQuantity < 10;
        public int DaysToExpiry => (ExpiryDate - DateTime.Now).Days;
    }
}
