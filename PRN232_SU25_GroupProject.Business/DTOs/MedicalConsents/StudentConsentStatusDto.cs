using PRN232_SU25_GroupProject.DataAccess.Enums;

namespace PRN232_SU25_GroupProject.Business.DTOs.MedicalConsents
{
    public class StudentConsentStatusDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }

        /// <summary>
        /// true = phụ huynh đã đồng ý;
        /// false = có bản ghi nhưng chưa đồng ý;
        /// null = chưa có giấy consent nào
        /// </summary>
        public bool? ConsentGiven { get; set; }

        public DateTime? ConsentDate { get; set; }

        public ConsentType ConsentType { get; set; }
        public int CampaignId { get; set; }
    }
}
