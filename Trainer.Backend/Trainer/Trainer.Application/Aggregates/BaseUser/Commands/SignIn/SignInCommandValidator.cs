using FluentValidation;
using Trainer.Application.Interfaces;
using Trainer.Common;

namespace Trainer.Application.Aggregates.BaseUser.Commands.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        private ITrainerDbContext DbContext
        {
            get;
        }

        public SignInCommandValidator(ITrainerDbContext dbContext)
        {
            DbContext = dbContext;

            RuleFor(x => x.Email)
                .EmailAddress()
                .Must(IsUniqueEmail);

            RuleFor(x => x.LastName)
                .NotNull()
                .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$");

            RuleFor(x => x.FirstName)
                .NotNull()
                .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$");

            RuleFor(x => x.MiddleName)
                .NotNull()
                .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .Must(PasswordsHelper.IsMeetsRequirements);

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .Equal(x => x.Password);
        }

        private bool IsUniqueEmail(string email)
        {
            return !DbContext.BaseUsers.Any(x => x.Email.Equals(email));
        }
    }
}
