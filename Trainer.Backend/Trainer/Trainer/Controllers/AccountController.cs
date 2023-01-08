using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Trainer.Application.Aggregates.BaseUser.Commands.SignIn;
using Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUser;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestLoginCode;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode;
using Trainer.Common;
using Trainer.Common.TableConnect.Common;
using Trainer.Enums;
using Trainer.Models;

namespace Trainer.Controllers
{
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(ILogger<AccountController> logger) : base(logger)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(SignInCommand command)
        {
            try
            {
                await Mediator.Send(command);
                await Mediator.Send(new RequestRegistrationCodeCommand
                {
                    Email = command.Email,
                    Host = HttpContext.Request.Host.ToString()
                });

                return RedirectToAction("VerifyCode", "OTP",
                    new {otpAction = OTPAction.Registration, email = command.Email});
            }
            catch (FluentValidation.ValidationException ex)
            {
                foreach (var modelValue in ModelState.Values)
                {
                    modelValue.Errors.Clear();
                }

                ModelState.AddModelError(string.Empty, ex.Errors.First().ErrorMessage);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var user = await Mediator.Send(new GetBaseUserQuery(model.Email));
                var result = CryptoHelper.VerifyHashedPassword(user.PasswordHash, model.Password);

                if (result)
                {
                    await Mediator.Send(new RequestLoginCodeCommand
                    {
                        Email = user.Email,
                        Host = HttpContext.Request.Host.ToString()
                    });
                    return Ok();
                    // return RedirectToAction("VerifyCode", "OTP",
                    // new { otpAction = OTPAction.Login, email = user.Email });
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error login/password");
            }
            return BadRequest("Error login/password");
        }

        [HttpGet("claim")]
        public async Task<IActionResult> ReturnClaim(string Email)
        {
            var user = await Mediator.Send(new GetBaseUserQuery(Email));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToName())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return Ok();
        }
    }
}