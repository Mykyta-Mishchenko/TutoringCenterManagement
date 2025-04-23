using backend.Mappers;
using JwtBackend.Mapping;

namespace backend.Extensions
{
    public static class MappingExtension
    {
        public static void AddApplicationMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMappingProfile));
            services.AddAutoMapper(typeof(LessonMappingProfile));
            services.AddAutoMapper(typeof(MarkTypeMappingProfile));
        }
    }
}
