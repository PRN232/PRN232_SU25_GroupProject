using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;

namespace PRN232_SU25_GroupProject.Business.DTOs.MedicalConsents
{
    public class CreateMedicalConsentClassRequest
    {
        [SwaggerSchemaExample("Vaccine")]
        public ConsentType ConsentType { get; set; }
        [SwaggerSchemaExample("1")]
        public int CampaignId { get; set; }

    }
}
