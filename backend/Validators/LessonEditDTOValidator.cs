using backend.DTO.LessonsDTO;
using FluentValidation;

namespace backend.Validators
{
    public class LessonEditDTOValidator: AbstractValidator<LessonEditDTO>
    {
        public LessonEditDTOValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Provide userId.");

            RuleFor(x => x.LessonId)
                .NotEmpty().WithMessage("Provide lessonId.");

            RuleFor(x => x.Price)
                .InclusiveBetween(0, 5000)
                .WithMessage("The lesson price must be between 0 and 5000.");

            RuleFor(x => x.SchoolYear)
                .InclusiveBetween(0, 12)
                .WithMessage("The school year must be between 0 and 12.");

            RuleFor(x => x.Day)
                .NotEmpty().WithMessage("Provide day.")
                .InclusiveBetween(1, 7)
                .WithMessage("The day must be between 1 and 7.");

            RuleFor(x => x.Hour)
                .NotEmpty().WithMessage("Provide hour.")
                .InclusiveBetween(8, 20)
                .WithMessage("The hour must be between 8 and 20.");

            RuleFor(x => x.Minutes)
                .InclusiveBetween(0, 60)
                .WithMessage("The minutes must be between 0 and 60.");

            RuleFor(x => x.MaxStudentsCount)
                .NotEmpty().WithMessage("Provide school max students count.")
                .InclusiveBetween(1, 5)
                .WithMessage("The max students count must be between 1 and 5.");
        }
    }
}
