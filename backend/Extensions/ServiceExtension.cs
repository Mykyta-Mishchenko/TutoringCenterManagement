using backend.Interfaces.Repositories;
using backend.Interfaces.Services;
using backend.Repositories;
using backend.Services;
using JwtBackend.Repositories;
using JwtBackend.Services;

namespace JwtBackend.Extensions
{
    public static class ServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ILessonsRepository, LessonsRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ILessonsService, LessonsService>();
        }
    }
}
