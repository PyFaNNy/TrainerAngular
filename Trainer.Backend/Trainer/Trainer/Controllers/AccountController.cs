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
            await Mediator.Send(command);
            await Mediator.Send(new RequestRegistrationCodeCommand
            {
                Email = command.Email,
                Host = HttpContext.Request.Host.ToString()
            });

            return Ok();
        }
    }
}