using backend.DTO.UsersDTO;
using FluentValidation;

namespace backend.Validators
{
    public class UsersFilterDTOValidator: AbstractValidator<UsersFilterDTO>
    {
        public UsersFilterDTOValidator()
        {
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required")
                .IsInEnum().WithMessage("Invalid role specified");

            RuleFor(x => x.Page)
                .NotEmpty().WithMessage("Page number is required");

            RuleFor(x => x.PerPage)
                .NotEmpty().WithMessage("Items number per page is required");

            RuleFor(x=>x.SchoolYear)
                .InclusiveBetween(0, 12)
                .WithMessage("The number must be between 0 and 12.");
        }
    }
}
