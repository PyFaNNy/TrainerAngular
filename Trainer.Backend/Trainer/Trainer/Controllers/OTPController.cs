using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestPassword;
using Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode;
using Trainer.Application.Exceptions;
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
        public async Task<IActionResult> ResetPasswordSendEmail(RequestPasswordCommand command)
        {
            try
            {
                command.Host = HttpContext.Request.Host.Value;
                await Mediator.Send(command);
                return RedirectToAction("VerifyCode", "OTP", new { otpAction = OTPAction.ResetPassword, email = command.Email });
            }
            catch (ValidationException ex)
            {
                // ModelState.AddModelError(string.Empty, Localizer[ex.Errors.FirstOrDefault().Key]);
            }
            catch (FluentValidation.ValidationException ex)
            {
                foreach (var modelValue in ModelState.Values)
                {
                    modelValue.Errors.Clear();
                }
                // ModelState.AddModelError(string.Empty, Localizer[ex.Errors.First().ErrorMessage]);
            }

            return Ok(command);
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
                // if (code.OTPaction == OTPAction.ResetPassword)
                // {
                //     return RedirectToAction("ResetPassword", "BaseUser", new { code.Email });
                // }
                //
                // if (OTPaction == OTPAction.Registration)
                // {
                //     return RedirectToAction("ConfirmEmail", "BaseUser", new { code.Email });
                // }
                //
                // if (OTPaction == OTPAction.Login)
                // {
                //     return RedirectToAction("ReturnClaim", "Account", new { code.Email });
                // }

                return Ok();
            }
            else
            {
                return BadRequest("Incorrect code");
            }
        }
    }
}
