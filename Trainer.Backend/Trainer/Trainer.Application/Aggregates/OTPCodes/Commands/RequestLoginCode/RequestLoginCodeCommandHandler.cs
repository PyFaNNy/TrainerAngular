using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestLoginCode
{
    public class RequestLoginCodeCommandHandler
        : RequestSmsCodeAbstractCommandHandler, IRequestHandler<RequestLoginCodeCommand, Unit>
    {
        public RequestLoginCodeCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper, IMailService emailService, 
            IOptions<OTPCodesErrorSettings> otpCodesErrorSettings)
            : base(mediator, dbContext, mapper, emailService, otpCodesErrorSettings)
        {
        }

        public async Task<Unit> Handle(RequestLoginCodeCommand request, CancellationToken cancellationToken)
        {
            if (OTPCodesErrorSettings.RequestLoginCodeEnable)
            {
                CredentialsMustBeValid(request);
                LimitsCodeValid(request);
                await CreateCode(request);
            }
            return Unit.Value;
        }

        private void CredentialsMustBeValid(RequestLoginCodeCommand request)
        {
            var user = DbContext.BaseUsers
                .Where(x => x.Email.Equals(request.Email))
                .FirstOrDefault();

            if (user == null)
            {
                throw new ValidationException( "Email","Wrong email");
            }
        }
    }
}
