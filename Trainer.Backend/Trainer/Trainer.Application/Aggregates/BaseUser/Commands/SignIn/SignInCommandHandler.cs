using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Domain.Entities.Doctor;
using Trainer.Domain.Entities.Manager;
using Trainer.Enums;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.BaseUser.Commands.SignIn
{
    public class SignInCommandHandler : AbstractRequestHandler, IRequestHandler<SignInCommand, Unit>
    {
        private readonly BaseUserErrorSettings BaseUserErrorSettings;

        public SignInCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<BaseUserErrorSettings> baseUserErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            BaseUserErrorSettings = baseUserErrorSettings.Value;
        }

        public async Task<Unit> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            if (BaseUserErrorSettings.SignInEnable)
            {
                if (request.Role == UserRole.Doctor)
                {
                    var doctor = Mapper.Map<Doctor>(request);

                    await DbContext.Doctors.AddAsync(doctor, cancellationToken);
                }

                if (request.Role == UserRole.Manager)
                {
                    var manager = Mapper.Map<Manager>(request);

                    await DbContext.Managers.AddAsync(manager, cancellationToken);
                }

                await DbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
