using backend.DTO.ReportsDTO;
using FluentValidation;

namespace backend.Validators
{
    public class ReportCreatingDTOValidator : AbstractValidator<ReportCreatingDTO>
    {
        public ReportCreatingDTOValidator()
        {
            RuleFor(x => x.TeacherId)
                .NotEmpty().WithMessage("Provide teacherId.");

            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("Provide studentId.");
            
            RuleFor(x => x.TeacherLessonId)
                .NotEmpty().WithMessage("Provide lessonId.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Provide date.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Provide description.")
                .MaximumLength(500).WithMessage("Maximum description length is 500 symbols.");
        }
    }
}
