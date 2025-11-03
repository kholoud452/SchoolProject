using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Services.Abstract;
using SchoolProject.Services.ImplementAbstract;


namespace SchoolProject.Services
{
    public static class ModuleServiceDependancies
    {
        public static IServiceCollection AddServiceDependancies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationCodeService, AuthorizationService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<ISubjectService, SubjectService>();
            return services;
        }
    }
}
