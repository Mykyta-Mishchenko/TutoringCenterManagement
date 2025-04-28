using backend.DTO.ExternalApiDTO;
using FluentValidation;

namespace backend.Validators
{
    public class ApiSignUpDTOValidator : AbstractValidator<ApiSignUpDTO>
    {
        public ApiSignUpDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("First Name is required")
                .Matches("^[a-zA-Z][a-zA-Z0-9_-]{3,}$")
                .WithMessage("Name must start with a letter and be at least 3 characters long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[_-])[A-Za-z\\d_-]{8,}$")
                .WithMessage("Password must be at least 8 characters long and contain uppercase, lowercase, number, and special character");
        }
    }
}
