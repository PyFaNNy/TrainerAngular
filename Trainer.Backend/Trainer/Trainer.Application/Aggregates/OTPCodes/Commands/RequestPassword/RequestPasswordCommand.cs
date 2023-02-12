using MediatR;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestPassword
{
    public class RequestPasswordCommand : RequestSmsCodeAbstractCommand, IRequest<Unit>
    {
        public RequestPasswordCommand()
        {
            Action = OTPAction.ResetPassword;
        }
    }
}
