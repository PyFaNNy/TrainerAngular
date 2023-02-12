using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.BaseUser.Commands.SignIn;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode;

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