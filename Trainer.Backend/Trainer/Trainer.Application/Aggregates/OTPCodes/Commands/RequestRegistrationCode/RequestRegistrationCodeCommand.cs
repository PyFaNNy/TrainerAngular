using MediatR;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode
{
    public class RequestRegistrationCodeCommand : RequestSmsCodeAbstractCommand, IRequest<Unit>
    {
        public RequestRegistrationCodeCommand()
        {
            Action = OTPAction.Registration;
        }
    }
}
