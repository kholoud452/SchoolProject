

using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Repositories;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependancies
    {
        public static IServiceCollection AddInfrastructureDependancies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepo, StudentRepo>();
            services.AddTransient<IDepertmentRepo, DepertmentRepo>();
            services.AddTransient<IInstructorRepo, InstructorRepo>();
            services.AddTransient<ISubjrctRepo, SubjectRepo>();
            services.AddTransient<IRefreshTokenRepo, RefreshTokenRepo>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }
    }
}
