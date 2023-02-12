using MediatR;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode
{
    public class ValidateSmsCodeQuery : IRequest<Code>
    {
        public ValidateSmsCodeQuery()
        {

        }

        public ValidateSmsCodeQuery(string email, string code, OTPAction action)
        {
            Email = email;
            Code = code;
            Action = action;
        }

        public string Email
        {
            get;
            set;
        }

        public string Code
        {
            get;
            set;
        }

        public OTPAction Action
        {
            get;
            set;
        }
    }
}
