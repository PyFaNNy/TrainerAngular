using App.Metrics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Trainer.Application.Aggregates.BaseUser.Commands.ApproveUser;
using Trainer.Application.Aggregates.BaseUser.Commands.BlockUser;
using Trainer.Application.Aggregates.BaseUser.Commands.ConfirmEmail;
using Trainer.Application.Aggregates.BaseUser.Commands.DeclineUser;
using Trainer.Application.Aggregates.BaseUser.Commands.DeleteUser;
using Trainer.Application.Aggregates.BaseUser.Commands.ResetPasswordUser;
using Trainer.Application.Aggregates.BaseUser.Commands.SignIn;
using Trainer.Application.Aggregates.BaseUser.Commands.UnBlockUser;
using Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUsers;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode;
using Trainer.Enums;
using Trainer.Metrics;

namespace Trainer.Controllers
{
    [ApiController]
    [Route("baseUser")]
    public class BaseUserController : BaseController
    {
        private readonly IMetrics _metrics;
        
        public BaseUserController(ILogger<BaseUserController> logger, IMetrics metrics)
            : base(logger)
        {
            _metrics = metrics;
        }

        [Authorize(Roles = "admin")]
        [HttpGet ("baseUser")]
        public async Task<IActionResult> GetModels(
            SortState sortOrder = SortState.FirstNameSort,
            int? pageIndex = 1,
            int? pageSize = 10)
        {
            _metrics.Measure.Counter.Increment(BusinessMetrics.BaseUserGetModels);
            
            var users = await Mediator.Send(new GetBaseUsersQuery(pageIndex, pageSize, sortOrder));
            return Ok(users);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("block")]
        public async Task<IActionResult> BlockUser(Guid[] selectedUsers)
        {
            _metrics.Measure.Counter.Increment(BusinessMetrics.BaseUserBlockUser);
            await Mediator.Send(new BlockUsersCommand {UserIds = selectedUsers});
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpPost("unBlock")]
        public async Task<IActionResult> UnBlockUser(Guid[] selectedUsers)
        {
            _metrics.Measure.Counter.Increment(BusinessMetrics.BaseUserUnBlockUser);
            await Mediator.Send(new UnBlockUsersCommand {UserIds = selectedUsers});
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete ("{selectedUsers}")]
        public async Task<IActionResult> DeleteUser(Guid[] selectedUsers)
        {
            _metrics.Measure.Counter.Increment(BusinessMetrics.BaseUserDeleteUser);
            await Mediator.Send(new DeleteUsersCommand {UserIds = selectedUsers});
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpGet("approve")]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            _metrics.Measure.Counter.Increment(BusinessMetrics.BaseUserApproveUser);
            await Mediator.Send(new ApproveUserCommand {UserId = new Guid(userId)});
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin")]
        [HttpGet("decline")]
        public async Task<IActionResult> DeclineUser(Guid userId)
        {
            _metrics.Measure.Counter.Increment(BusinessMetrics.BaseUserDeclineUser);
            await Mediator.Send(new DeclineUserCommand {UserId = userId});
            return RedirectToAction("GetModels");
        }
        
        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordUserCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email)
        {
            await Mediator.Send(new ConfirmEmailCommand {Email = email});
            return Ok();
        }

    }
}