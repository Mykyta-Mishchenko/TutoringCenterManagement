using backend.DTO.ReportsDTO;
using FluentValidation;

namespace backend.Validators
{
    public class ReportEditingDTOValidator : AbstractValidator<ReportEditingDTO>
    {
        public ReportEditingDTOValidator()
        {
            RuleFor(x => x.ReportId)
                .NotEmpty().WithMessage("Provide reportId.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Provide description.")
                .MaximumLength(500).WithMessage("Maximum description length is 500 symbols.");
        }
    }
}
