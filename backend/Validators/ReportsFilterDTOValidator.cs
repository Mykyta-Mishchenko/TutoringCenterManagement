using backend.DTO.ReportsDTO;
using FluentValidation;

namespace backend.Validators
{
    public class ReportsFilterDTOValidator : AbstractValidator<ReportsFilterDTO>
    {
        public ReportsFilterDTOValidator() {
            RuleFor(x => x.Page)
                .NotEmpty().WithMessage("Page number is required");

            RuleFor(x => x.PerPage)
                .NotEmpty().WithMessage("Items number per page is required");
        }
    }
}
