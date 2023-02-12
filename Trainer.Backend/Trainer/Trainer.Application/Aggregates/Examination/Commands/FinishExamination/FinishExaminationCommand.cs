using MediatR;

namespace Trainer.Application.Aggregates.Examination.Commands.FinishExamination
{
    public class FinishExaminationCommand : IRequest<Unit>
    {
        public Guid ExaminationId
        {
            get;
            set;
        }
    }
}
