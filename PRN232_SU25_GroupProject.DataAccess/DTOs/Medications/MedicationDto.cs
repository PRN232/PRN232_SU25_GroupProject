namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Medications
{
    public class MedicationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string StorageInstructions { get; set; }
        public bool IsExpired => ExpiryDate < DateTime.Now;
        public bool IsLowStock => StockQuantity < 10;
        public int DaysToExpiry => (ExpiryDate - DateTime.Now).Days;
    }
}
