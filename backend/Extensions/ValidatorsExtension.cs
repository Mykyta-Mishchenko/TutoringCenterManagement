using backend.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using JwtBackend.Validators;

namespace backend.Extensions
{
    public static class ValidatorsExtension
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<SignUpDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<SignInDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<UserProfileValidator>();

            services.AddValidatorsFromAssemblyContaining<UsersFilterDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<LessonEditDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<LessonCreateDTOValidator>();

            services.AddValidatorsFromAssemblyContaining<ReportsFilterDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<ReportCreatingDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<ReportEditingDTOValidator>();
        }
    }
}
