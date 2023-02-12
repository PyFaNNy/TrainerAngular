using AutoMapper;
using MediatR;
using Trainer.Application.Mappings;
using Trainer.Domain.Entities.Result;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Results.Commands.Create
{
    public class CreateResultsCommand : IRequest<Unit>, IMapTo<Result>
    {
        public Guid Id
        {
            get;
            set;
        }

        public Guid PatientId
        {
            get;
            set;
        }

        public Guid ExaminationId
        {
            get;
            set;
        }

        public int AverageHeartRate
        {
            get;
            set;
        }
        public int AverageDia
        {
            get;
            set;
        }

        public int AverageSis
        {
            get;
            set;
        }

        public int AverageOxigen
        {
            get;
            set;
        }
        public double AverageTemperature
        {
            get;
            set;
        }

        public TypePhysicalActive TypePhysicalActive
        {
            get;
            set;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateResultsCommand, Result>()
                .ForMember(d => d.AverageOxigen, opt => opt.MapFrom(s => s.AverageOxigen))
                .ForMember(d => d.AverageTemperature, opt => opt.MapFrom(s => s.AverageTemperature))
                .ForMember(d => d.AverageSis, opt => opt.MapFrom(s => s.AverageSis))
                .ForMember(d => d.AverageHeartRate, opt => opt.MapFrom(s => s.AverageHeartRate))
                .ForMember(d => d.PatientId, opt => opt.MapFrom(s => s.PatientId))
                .ForMember(d => d.Date, opt => opt.MapFrom(s => DateTime.Now))
                .ForMember(d => d.TypePhysicalActive, opt => opt.MapFrom(s => s.TypePhysicalActive))
                .ForMember(d => d.ExaminationId, opt => opt.MapFrom(s => s.ExaminationId));
        }
    }
}
