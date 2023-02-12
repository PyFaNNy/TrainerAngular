using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.BaseUser.Commands.ConfirmEmail;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestPassword;
using Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode;
using Trainer.Enums;
using Trainer.Models;

namespace Trainer.Controllers
{
    [ApiController]
    [Route("otp")]
    public class OTPController : BaseController
    {
        public OTPController(ILogger<OTPController> logger) : base(logger)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("reset")]
        public async Task<IActionResult> ResetPasswordSendEmail(string email)
        {
            await Mediator.Send(new RequestPasswordCommand
            {
                Host = HttpContext.Request.Host.Value,
                Email = email
            });
            return Ok();
        }

        /// <summary>
        /// Verify Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyCode(ValidateSmsCode code)
        {
            var result = await Mediator.Send(new ValidateSmsCodeQuery
            {
                Code = code.Code,
                Email = code.Email,
                Action = code.OTPaction
            });
            if (result.IsValid)
            {
                if (code.OTPaction == OTPAction.Registration)
                {
                    await Mediator.Send(new ConfirmEmailCommand {Email = code.Email});
                }

                return Ok();
            }

            return BadRequest("Incorrect code");
        }
    }
}