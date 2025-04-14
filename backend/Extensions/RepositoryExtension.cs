using backend.Interfaces.Repositories;
using backend.Repositories;
using JwtBackend.Repositories;
using System.Runtime.CompilerServices;

namespace backend.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ILessonsRepository, LessonsRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ISubjectsRepository, SubjectsRepository>();
            services.AddScoped<ILessonTypeRepository, LessonTypeRepository>();
        }
    }
}
