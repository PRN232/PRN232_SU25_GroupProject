using PRN232_SU25_GroupProject.Business.Mappings;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.Business.Service.Services;
using PRN232_SU25_GroupProject.DataAccess.Repositories;
using PRN232_SU25_GroupProject.DataAccess.Repository.Interfaces;
using PRN232_SU25_GroupProject.DataAccess.Repository.Repositories;

namespace PRN232_SU25_GroupProject.Presentation.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { cfg.AddProfile<MappingProfile>(); }, typeof(MappingProfile).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            ///////////////////////////////////////////////////////
            ///                      DAOS                      ///
            /////////////////////////////////////////////////////



            ////////////////////////////////////////////////////
            ///                 REPOSITORIES                ///
            //////////////////////////////////////////////////
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IStudentRepository, StudentRepository>();


            ////////////////////////////////////////////////
            ///                 SERVICES                ///
            //////////////////////////////////////////////
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IParentService, ParentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IHealthCheckupService, HealthCheckupService>();
            services.AddScoped<IMedicalIncidentService, MedicalIncidentService>();
            services.AddScoped<IMedicalProfileService, MedicalProfileService>();
            services.AddScoped<IMedicationService, MedicationService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IStudentMedicationService, StudentMedicationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVaccinationService, VaccinationService>();



            return services;
        }
    }
}
