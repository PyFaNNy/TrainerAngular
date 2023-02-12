using MediatR;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestLoginCode
{
    public class RequestLoginCodeCommand :  RequestSmsCodeAbstractCommand, IRequest<Unit>
    {
        public RequestLoginCodeCommand()
        {
            Action = OTPAction.Login;
        }
    }
}
