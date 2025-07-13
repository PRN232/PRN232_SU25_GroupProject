using AutoMapper;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication;
using PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalConsents;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.Allergy;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.ChronicDisease;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.MedicalHistory;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicationGivens;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Medications;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Notifications;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Parents;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Reports;
using PRN232_SU25_GroupProject.DataAccess.DTOs.StudentMedications;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Users;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ////////////////////////////////////////////////////
            ///             USER & AUTH MAPPING              ///
            ////////////////////////////////////////////////////
            CreateMap<User, UserDto>()
    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<CreateUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<LoginRequest, User>().ReverseMap();
            CreateMap<LoginResult, UserDto>().ReverseMap();

            ////////////////////////////////////////////////////
            ///              STUDENT & PARENT                ///
            ////////////////////////////////////////////////////
            CreateMap<Student, StudentDto>()
    .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.FullName))
    .ForMember(dest => dest.ParentPhone, opt => opt.MapFrom(src => src.Parent.PhoneNumber))
    .ForMember(dest => dest.HasMedicalProfile, opt => opt.MapFrom(src => src.MedicalProfile != null));

            CreateMap<CreateStudentRequest, Student>();
            CreateMap<UpdateStudentRequest, Student>();

            CreateMap<Parent, ParentDto>()
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.UserName))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));
            CreateMap<UpdateParentRequest, Parent>();

            ////////////////////////////////////////////////////
            ///           MEDICAL PROFILE MAPPING            ///
            ////////////////////////////////////////////////////
            CreateMap<MedicalProfile, MedicalProfileDto>().ReverseMap();
            CreateMap<Allergy, AllergyDto>().ReverseMap();
            CreateMap<ChronicDisease, ChronicDiseaseDto>().ReverseMap();
            CreateMap<MedicalHistory, MedicalHistoryDto>().ReverseMap();

            CreateMap<VaccinationRecord, VaccinationRecordDto>().ReverseMap();



            CreateMap<UpdateMedicalProfileRequest, MedicalProfile>();
            CreateMap<CreateAllergyRequest, Allergy>();
            CreateMap<UpdateAllergyRequest, Allergy>();
            CreateMap<CreateChronicDiseaseRequest, ChronicDisease>();
            CreateMap<UpdateChronicDiseaseRequest, ChronicDisease>();
            CreateMap<CreateMedicalHistoryRequest, MedicalHistory>();
            CreateMap<UpdateMedicalHistoryRequest, MedicalHistory>();

            ////////////////////////////////////////////////////
            ///           MEDICAL INCIDENT MAPPING           ///
            ////////////////////////////////////////////////////
            CreateMap<MedicalIncident, MedicalIncidentDto>();
            CreateMap<CreateMedicalIncidentRequest, MedicalIncident>();
            CreateMap<UpdateMedicalIncidentRequest, MedicalIncident>();

            CreateMap<MedicationGiven, MedicationGivenDto>().ReverseMap();
            CreateMap<CreateMedicationsGivenRequest, MedicationGiven>();
            CreateMap<UpdateMedicationsGivenRequest, MedicationGiven>();

            ////////////////////////////////////////////////////
            ///           MEDICATION MAPPING                 ///
            ////////////////////////////////////////////////////
            CreateMap<Medication, MedicationDto>();
            CreateMap<Medication, MedicationDto>().ReverseMap();

            CreateMap<StudentMedication, StudentMedicationDto>();
           
              

            CreateMap<AddMedicationRequest, Medication>();

            CreateMap<MedicalConsent, MedicalConsentDto>()
                .ForMember(dest => dest.StudentName, opt => opt.Ignore());
            CreateMap<CreateMedicalConsentRequest, MedicalConsent>();
            CreateMap<UpdateMedicalConsentRequest, MedicalConsent>();
            CreateMap<ApproveStudentMedicationRequest, StudentMedication>();
            CreateMap<UpdateStudentMedicationRequest, StudentMedication>();
            CreateMap<CreateStudentMedicationRequest, StudentMedication>();

            ////////////////////////////////////////////////////
            ///           VACCINATION MAPPING                ///
            ////////////////////////////////////////////////////
            CreateMap<VaccinationCampaign, VaccinationCampaignDto>();
            CreateMap<CreateVaccinationCampaignRequest, VaccinationCampaign>();
            CreateMap<UpdateVaccinationCampaignRequest, VaccinationCampaign>();
            CreateMap<MedicalConsent, MedicalConsentDto>();

            CreateMap<VaccinationRecord, VaccinationRecordDto>();
            CreateMap<RecordVaccinationRequest, VaccinationRecord>();
            CreateMap<CreateVaccinationRecordRequest, VaccinationRecord>();
            CreateMap<UpdateVaccinationRecordRequest, VaccinationRecord>();


            ////////////////////////////////////////////////////
            ///           HEALTH CHECKUP MAPPING             ///
            ////////////////////////////////////////////////////
            CreateMap<CreateCheckupCampaignRequest, HealthCheckupCampaign>();
            CreateMap<UpdateCheckupCampaignRequest, HealthCheckupCampaign>();
            CreateMap<HealthCheckupCampaign, HealthCheckupCampaignDto>();


            CreateMap<RecordCheckupRequest, HealthCheckupResult>();

            CreateMap<UpdateCheckupResultRequest, HealthCheckupResult>();
            CreateMap<HealthCheckupResult, HealthCheckupResultDto>()
                .ForMember(dest => dest.StudentName, opt => opt.Ignore())
                .ForMember(dest => dest.StudentCode, opt => opt.Ignore())
                .ForMember(dest => dest.CampaignName, opt => opt.Ignore())
                .ForMember(dest => dest.NurseName, opt => opt.Ignore());


            ////////////////////////////////////////////////////
            ///           NOTIFICATION MAPPING               ///
            ////////////////////////////////////////////////////
            CreateMap<Notification, NotificationDto>().ReverseMap();

            ////////////////////////////////////////////////////
            ///             REPORTING MAPPING                ///
            ////////////////////////////////////////////////////
            CreateMap<MedicalIncident, RecentIncident>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity.ToString()))
                .ForMember(dest => dest.IncidentDate, opt => opt.MapFrom(src => src.IncidentDate));

            CreateMap<MedicalIncident, MedicalIncidentDto>();
            CreateMap<StudentMedication, StudentMedicationDto>();
            CreateMap<HealthCheckupResult, HealthCheckupResultDto>();
            CreateMap<VaccinationRecord, VaccinationRecordDto>();
        }
    }

}
