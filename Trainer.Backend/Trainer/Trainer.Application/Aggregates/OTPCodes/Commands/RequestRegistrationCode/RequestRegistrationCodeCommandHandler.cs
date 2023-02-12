using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode
{
    public class RequestRegistrationCodeCommandHandler : RequestSmsCodeAbstractCommandHandler, IRequestHandler<RequestRegistrationCodeCommand, Unit>
    {
        public RequestRegistrationCodeCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IMailService emailService,
            IOptions<OTPCodesErrorSettings> otpCodesErrorSettings)
        : base(mediator, dbContext, mapper, emailService, otpCodesErrorSettings)
        {
        }

        public async Task<Unit> Handle(RequestRegistrationCodeCommand request, CancellationToken cancellationToken)
        {
            if (OTPCodesErrorSettings.RequestRegistrationCodeEnable)
            {
                LimitsCodeValid(request);
                await CreateCode(request);
            }
            return Unit.Value;
        }
    }
}
