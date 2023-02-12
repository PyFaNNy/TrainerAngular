using FluentValidation;

namespace Trainer.Application.Aggregates.OTPCodes
{
    public abstract class RequestSmsCodeAbstractCommandValidator<T>
        : AbstractValidator<T> where T : RequestSmsCodeAbstractCommand
    {
        public RequestSmsCodeAbstractCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();
        }
    }
}
