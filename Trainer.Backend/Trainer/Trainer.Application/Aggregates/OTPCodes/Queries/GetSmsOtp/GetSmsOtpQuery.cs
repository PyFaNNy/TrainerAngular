using MediatR;
using Trainer.Domain.Entities;
using Trainer.Enums;

namespace Prixy.Application.Aggregates.OTPCodes.Queries.GetSmsOtp;

public class GetSmsOtpQuery: IRequest<OTP>
{
    public GetSmsOtpQuery(string email, string code, OTPAction action)
    {
        Email = email;
        Code = code;
        Action = action;
    }

    public string Email
    {
        get;
    }

    public string Code
    {
        get;
    }

    public OTPAction Action
    {
        get;
    }
}
