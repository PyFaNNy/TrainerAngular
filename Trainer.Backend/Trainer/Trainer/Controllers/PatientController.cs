using App.Metrics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.CSV.Commands.CSVToPatients;
using Trainer.Application.Aggregates.CSV.Queries.PatientsToCSV;
using Trainer.Application.Aggregates.Patient.Commands.CreatePatient;
using Trainer.Application.Aggregates.Patient.Commands.DeletePatient;
using Trainer.Application.Aggregates.Patient.Commands.UpdatePatient;
using Trainer.Application.Aggregates.Patient.Queries.GetPatient;
using Trainer.Application.Aggregates.Patient.Queries.GetPatients;
using Trainer.Application.Exceptions;
using Trainer.Application.Models;
using Trainer.Common;
using Trainer.Enums;
using Trainer.Metrics;
using Trainer.Models;
using Patient = Trainer.Application.Aggregates.Patient.Queries.GetPatients.Patient;

namespace Trainer.Controllers;

[ApiController]
[Route("patient")]
public class PatientController : BaseController
{
    private readonly IMetrics _metrics;
    
    public PatientController(ILogger<PatientController> logger,
        IMetrics metrics)
        : base(logger)
    {
        _metrics = metrics;
    }

    /// <summary>
    ///  Get all patients
    /// </summary>
    /// <param name="sortOrder"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "admin, doctor, manager")]
    public async Task<ActionResult<PaginatedList<Patient>>> GetModels(
        SortState sortOrder = SortState.FirstNameSortAsc,
        int? pageIndex = 1,
        int? pageSize = 10)
    {
        _metrics.Measure.Counter.Increment(BusinessMetrics.PatientGetModels);
        var results = await Mediator.Send(new GetPatientsQuery(pageIndex, pageSize, sortOrder));
        return Ok(results);
    }

    /// <summary>
    /// Get patient
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "admin, doctor, manager")]
    public async Task<ActionResult<Application.Aggregates.Patient.Queries.GetPatient.Patient>> GetModel(Guid id)
    {
        _metrics.Measure.Counter.Increment(BusinessMetrics.PatientGetModel);
        var patient = await Mediator.Send(new GetPatientQuery {PatientId = id});
        return Ok(patient);
    }
    
    /// <summary>
    /// Create patient
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "admin, manager")]
    public async Task<IActionResult> AddModel(CreatePatientCommand command)
    {
        _metrics.Measure.Counter.Increment(BusinessMetrics.PatientAddModel);
        try
        {
            await Mediator.Send(command);
            return RedirectToAction("GetModels");
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError("All", ex.Errors.FirstOrDefault().Value);
        }
        catch (FluentValidation.ValidationException ex)
        {
            foreach (var modelValue in ModelState.Values) modelValue.Errors.Clear();
        }

        return Ok();
    }
    
    /// <summary>
    /// Update patient
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = "admin, manager")]
    public async Task<IActionResult> UpdateModel(UpdatePatientCommand command)
    {
        _metrics.Measure.Counter.Increment(BusinessMetrics.PatientUpdateModel);
        try
        {
            await Mediator.Send(command);
            return Ok();
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError("All", ex.Errors.FirstOrDefault().Value);
        }
        catch (FluentValidation.ValidationException ex)
        {
            foreach (var modelValue in ModelState.Values) modelValue.Errors.Clear();
        }
        return BadRequest();
    }

    /// <summary>
    /// Delete patients
    /// </summary>
    /// <param name="selectedPatient"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin, manager")]
    [HttpDelete]
    public async Task<IActionResult> DeleteModelAsync(Guid[] selectedPatient)
    {
        _metrics.Measure.Counter.Increment(BusinessMetrics.PatientDeleteModel);
        await Mediator.Send(new DeletePatientsCommand {PatientsId = selectedPatient});
        return NoContent();
    }

    /// <summary>
    /// Export patient to csv
    /// </summary>
    /// <returns></returns>
    [HttpGet("export")]
    [Authorize(Roles = "admin, manager")]
    public async Task<IActionResult> ExportToCSV()
    {
        var fileInfo = await Mediator.Send(new PatientsToCSVQuery());
        return File(fileInfo.Content, fileInfo.Type.ToName(), fileInfo.FileName);
    }
    
    /// <summary>
    /// Import patient csv to dataBase
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    [HttpPost("import")]
    [Authorize(Roles = "admin, manager")]
    public async Task<IActionResult> ImportToCSV(CSV source)
    {
        await Mediator.Send(new CSVToPatientsCommand {CSVFile = source.File});
        return Ok();
    }
}