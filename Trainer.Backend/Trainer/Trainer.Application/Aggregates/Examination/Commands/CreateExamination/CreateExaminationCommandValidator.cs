using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Trainer.Application.Aggregates.Examination.Commands.CreateExamination
{
    public class CreateExaminationCommandValidator : AbstractValidator<CreateExaminationCommand>
    {
        public CreateExaminationCommandValidator()
        {
            RuleFor(x => x.Date)
                .Must(ValidateDate)
                .WithMessage("Wrong date");
            RuleFor(x => x.Indicators)
                .GreaterThan(0)
                .WithMessage("You must select at least one sensor");
        }

        private bool ValidateDate(DateTime date)
        {
            return date > DateTime.Now;
        }
    }
}
