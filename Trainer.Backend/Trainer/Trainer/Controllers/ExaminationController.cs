using System.Data;
using App.Metrics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Trainer.Application.Exceptions;
using Trainer.Application.Aggregates.CSV.Commands.CSVToExaminations;
using Trainer.Application.Aggregates.CSV.Queries.ExaminationsToCSV;
using Trainer.Application.Aggregates.Examination.Commands.CreateExamination;
using Trainer.Application.Aggregates.Examination.Commands.DeleteExamination;
using Trainer.Application.Aggregates.Examination.Commands.UpdateExamination;
using Trainer.Application.Aggregates.Examination.Queries.GetExamination;
using Trainer.Application.Aggregates.Examination.Queries.GetExaminations;
using Trainer.Application.Interfaces;
using Trainer.Common;
using Trainer.Enums;
using Trainer.Infrastructure.Extensions;
using Trainer.Metrics;
using Trainer.Models;

namespace Trainer.Controllers
{

    public class ExaminationController : BaseController
    {
        private readonly IStringLocalizer<ExaminationController> Localizer;
        private readonly IMetrics _metrics;

        public ExaminationController(ILogger<ExaminationController> logger,
            IStringLocalizer<ExaminationController> localizer,
            IMetrics metrics)
            : base(logger)
        {
            Localizer = localizer;
            _metrics = metrics;
        }

        [HttpGet]
        [Authorize(Roles = "admin, doctor, manager")]
        public async Task<IActionResult> GetModels(
            SortState sortOrder = SortState.FirstNameSort,
            int? pageIndex = 1,
            int? pageSize = 10)
        {
            _metrics.Measure.Counter.Increment(BusinessMetrics.ExaminationGetModels);

            var result = await Mediator.Send(new GetExaminationsQuery(pageIndex, pageSize, sortOrder));
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin, doctor, manager")]
        public async Task<IActionResult> GetModel(Guid id)
        {
            _metrics.Measure.Counter.Increment(BusinessMetrics.ExaminationGetModel);
            var result = await Mediator.Send(new GetExaminationQuery { ExaminationId = id });
            //ViewBag.Id = result.Id;
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "doctor")]
        public async Task<IActionResult> AddModel(Guid id)
        {
            //ViewBag.UserId = id;
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "doctor")]
        public async Task<IActionResult> AddModel(CreateExaminationCommand command)
        {
            _metrics.Measure.Counter.Increment(BusinessMetrics.ExaminationAddModel);
            try
            {
                var doctorId = this.HttpContext.User.GetUserId();
                command.DoctorId = doctorId.Value;
                await Mediator.Send(command);

                return RedirectToAction("GetModels");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, Localizer[ex.Errors.FirstOrDefault().Key]);
            }
            catch (FluentValidation.ValidationException ex)
            {
                foreach (var modelValue in ModelState.Values)
                {
                    modelValue.Errors.Clear();
                }
                ModelState.AddModelError(string.Empty, Localizer[ex.Errors.First().ErrorMessage]);
            }

            //ViewBag.UserId = command.PatientId;
            return Ok(command);
        }

        [HttpGet]
        [Authorize(Roles = "doctor")]
        public async Task<IActionResult> UpdateModel(Guid id)
        {
            var examination = await Mediator.Send(new GetExaminationQuery { ExaminationId = id });
            //ViewBag.Examination = examination;
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "doctor")]
        public async Task<IActionResult> UpdateModel(UpdateExaminationCommand command)
        {
            _metrics.Measure.Counter.Increment(BusinessMetrics.ExaminationUpdateModel);
            try
            {
                var doctorId = this.HttpContext.User.GetUserId();
                command.DoctorId = doctorId.Value;
                await Mediator.Send(command);

                return RedirectToAction("GetModels");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, Localizer[ex.Errors.FirstOrDefault().Key]);
            }
            catch (FluentValidation.ValidationException ex)
            {
                foreach (var modelValue in ModelState.Values)
                {
                    modelValue.Errors.Clear();
                }
                ModelState.AddModelError(string.Empty, Localizer[ex.Errors.First().ErrorMessage]);
            }
            //ViewBag.Examination = command;
            return Ok(command);
        }

        [Authorize(Roles = "doctor")]
        public async Task<RedirectToActionResult> DeleteModel(Guid[] selectedExamination)
        {
            _metrics.Measure.Counter.Increment(BusinessMetrics.ExaminationDeleteModel);
            await Mediator.Send(new DeleteExaminationsCommand { ExaminationsId = selectedExamination });
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> ExportToCSV()
        {
            var fileInfo =await Mediator.Send(new ExaminationsToCSVQuery());
            return File(fileInfo.Content, fileInfo.Type.ToName(), fileInfo.FileName);
        }

        [HttpGet]
        public async Task<IActionResult> ImportToCSV()
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> ImportToCSV(CSV source)
        {
            await Mediator.Send(new CSVToExaminationsCommand { CSVFile = source.File });
            return RedirectToAction("GetModels");
        }
    }
}
