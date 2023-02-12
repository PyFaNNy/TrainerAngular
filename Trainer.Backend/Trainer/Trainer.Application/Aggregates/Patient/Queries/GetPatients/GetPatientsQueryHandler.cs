using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Extensions.IQueryableExtensions;
using Trainer.Application.Interfaces;
using Trainer.Application.Models;
using Trainer.Enums;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Patient.Queries.GetPatients
{
    public class GetPatientsQueryHandler : AbstractRequestHandler, IRequestHandler<GetPatientsQuery, PaginatedList<Patient>>
    {
        private readonly PatientErrorSettings PatientErrorSettings;

        public GetPatientsQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<PatientErrorSettings> patientErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            PatientErrorSettings = patientErrorSettings.Value;
        }

        public async Task<PaginatedList<Patient>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            if (PatientErrorSettings.GetPatientsEnable)
            {
                var patients = DbContext.Patients
                    .NotRemoved()
                    .ProjectTo<Patient>(Mapper.ConfigurationProvider);

                var paginatedList =
                    await PaginatedList<Patient>.CreateAsync(patients, request.PageIndex, request.PageSize);

                switch (request.SortOrder)
                {
                    case SortState.FirstNameSortAsc:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.FirstName).ToList();
                        break;
                    case SortState.FirstNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.FirstName).ToList();
                        break;
                    case SortState.MiddleNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.MiddleName).ToList();
                        break;
                    case SortState.MiddleNameSortAsc:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.MiddleName).ToList();
                        break;
                    case SortState.LastNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.LastName).ToList();
                        break;
                    case SortState.LastNameSortAsc:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.LastName).ToList();
                        break;
                    case SortState.AgeSortAsc:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Age).ToList();
                        break;
                    case SortState.AgeSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Age).ToList();
                        break;
                    case SortState.SexSortAsc:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Sex).ToList();
                        break;
                    case SortState.SexSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Sex).ToList();
                        break;
                }

                return paginatedList;
            }

            if (PatientErrorSettings.GetRandomPatientsEnable)
            {
                Random rnd = new Random();
                var patients = DbContext.Patients
                    .Skip(rnd.Next(30))
                    .NotRemoved()
                    .ProjectTo<Patient>(Mapper.ConfigurationProvider);

                patients = patients.Skip(rnd.Next(patients.Count()));

                var paginatedList =
                    await PaginatedList<Patient>.CreateAsync(patients, request.PageIndex, request.PageSize);

                switch (request.SortOrder)
                {
                    case SortState.FirstNameSortAsc:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.FirstName).ToList();
                        break;
                    case SortState.FirstNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.FirstName).ToList();
                        break;
                    case SortState.MiddleNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.MiddleName).ToList();
                        break;
                    case SortState.MiddleNameSortAsc:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.MiddleName).ToList();
                        break;
                    case SortState.LastNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.LastName).ToList();
                        break;
                    case SortState.LastNameSortAsc:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.LastName).ToList();
                        break;
                    case SortState.AgeSortAsc:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Age).ToList();
                        break;
                    case SortState.AgeSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Age).ToList();
                        break;
                    case SortState.SexSortAsc:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Sex).ToList();
                        break;
                    case SortState.SexSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Sex).ToList();
                        break;
                }

                return paginatedList;
            }

            return null;
        }
    }
}
