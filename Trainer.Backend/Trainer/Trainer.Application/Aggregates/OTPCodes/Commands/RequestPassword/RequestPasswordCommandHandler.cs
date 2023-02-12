using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestPassword
{
    public class RequestPasswordCommandHandler : RequestSmsCodeAbstractCommandHandler, IRequestHandler<RequestPasswordCommand, Unit>
    {
        public RequestPasswordCommandHandler(IMediator mediator, ITrainerDbContext dbContext, IMapper mapper, IMailService emailService,
        IOptions<OTPCodesErrorSettings> otpCodesErrorSettings)
            : base(mediator, dbContext, mapper, emailService, otpCodesErrorSettings)
        {
        }

        public async Task<Unit> Handle(RequestPasswordCommand request, CancellationToken cancellationToken)
        {
            if (OTPCodesErrorSettings.RequestPasswordEnable)
            {
                CheckIfUserExists(request.Email);
                LimitsCodeValid(request);
                await CreateCode(request);
            }

            return Unit.Value;
        }

        private void CheckIfUserExists(string email)
        {
            var isUserExist = DbContext.BaseUsers.Any(x => x.Email == email);

            if (!isUserExist)
            {
                throw new ValidationException("IncorrectEmail", "Incorrect email");
            }
        }
    }
}
