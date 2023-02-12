using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode
{
    public class ValidateSmsCodeQueryHandler
        : AbstractRequestHandler, IRequestHandler<ValidateSmsCodeQuery, Code>
    {
        private readonly OTPCodesErrorSettings OTPCodesErrorSettings;

        public ValidateSmsCodeQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<OTPCodesErrorSettings> otpCodesErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            OTPCodesErrorSettings = otpCodesErrorSettings.Value;
        }

        public async Task<Code> Handle(ValidateSmsCodeQuery request, CancellationToken cancellationToken)
        {
            var isEmailExisted = DbContext.BaseUsers
                .Any(x => x.Email.Equals(request.Email));

            if (!isEmailExisted)
            {
                throw new NotFoundException(nameof(Domain.Entities.BaseUser.Email), request.Email);
            }

            if (OTPCodesErrorSettings.IsUniversalVerificationCodeEnabled && request.Code.Equals(OTPCodesErrorSettings.UniversalVerificationCode))
            {
                return new Code
                {
                    CodeValue = request.Code,
                    IsValid = true
                };
            }

            var code = await DbContext.OTPs
                .Where(x => x.Email == request.Email)
                .Where(x => x.Action == request.Action)
                .Where(x => x.CreatedAt > DateTime.UtcNow.AddMinutes(-5))
                .Where(x => x.Value == request.Code)
                .FirstOrDefaultAsync(cancellationToken);

            bool isValid;
            if (code == null)
            { 
                isValid = false;
            }
            else
            {
                isValid = code.IsValid;
                code.IsValid = false;

                await DbContext.SaveChangesAsync(cancellationToken);
            }
            
            return new Code
            {
                CodeValue = request.Code,
                IsValid = isValid
            };
        }
    }
}
