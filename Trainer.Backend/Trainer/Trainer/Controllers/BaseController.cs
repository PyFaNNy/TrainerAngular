using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Trainer.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ILogger Logger
        {
            get;
        }

        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected BaseController(ILogger logger)
        {
            Logger = logger;
        }
    }
}
