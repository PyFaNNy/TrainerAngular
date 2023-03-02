using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigController : BaseController
{
    private readonly IWritableOptions<ExaminationErrorSettings> _examinationErrorSettings;
    private readonly IWritableOptions<PatientErrorSettings> _patientErrorSettings;
    private readonly IWritableOptions<ResultsErrorSettings> _resultsErrorSettings;
    private readonly IWritableOptions<OTPCodesErrorSettings> _OTPCodesErrorSettings;
    private readonly IWritableOptions<CSVErrorSettings> _CSVErrorSettings;
    private readonly IWritableOptions<BaseUserErrorSettings> _baseUserErrorSettings;
    
    public ConfigController(ILogger<AccountController> logger,
        IWritableOptions<ExaminationErrorSettings> examinationErrorSettings,
        IWritableOptions<PatientErrorSettings> patientErrorSettings,
        IWritableOptions<ResultsErrorSettings> resultsErrorSettings,
        IWritableOptions<OTPCodesErrorSettings> OTPCodesErrorSettings,
        IWritableOptions<CSVErrorSettings> CSVErrorSettings,
        IWritableOptions<BaseUserErrorSettings> baseUserErrorSettings) : base(logger)
    {
        _examinationErrorSettings = examinationErrorSettings;
        _patientErrorSettings = patientErrorSettings;
        _resultsErrorSettings = resultsErrorSettings;
        _CSVErrorSettings = CSVErrorSettings;
        _OTPCodesErrorSettings = OTPCodesErrorSettings;
        _baseUserErrorSettings = baseUserErrorSettings;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("examination")]
    public async Task<ActionResult<ExaminationErrorSettings>> GetExaminationErrorSettings()
    {
        return Ok(_examinationErrorSettings.Value);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("examination")]
    public async Task<ActionResult<ExaminationErrorSettings>> SetExaminationErrorSettings(ExaminationErrorSettings model)
    {
        _examinationErrorSettings.Update(t =>
        {
            t.CreateExaminationEnable = model.CreateExaminationEnable;
            t.CreateEmailExaminationEnable = model.CreateEmailExaminationEnable;
            t.GetExaminationEnable = model.GetExaminationEnable;
            t.GetExaminationsEnable = model.GetExaminationsEnable;
            t.DeleteExaminationEnable = model.DeleteExaminationEnable;
            t.UpdateExaminationEnable = model.UpdateExaminationEnable;
            t.UpdateEmailExaminationEnable = model.UpdateEmailExaminationEnable;
            t.FinishExaminationEnable = model.FinishExaminationEnable;
            t.GetRandomExaminationEnable = model.GetRandomExaminationEnable;
            t.GetRandomExaminationsEnable = model.GetRandomExaminationsEnable;
        });
        return Ok(model);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("patient")]
    public async Task<ActionResult<PatientErrorSettings>> GetPatientErrorSettings()
    {
        return Ok(_examinationErrorSettings.Value);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("patient")]
    public async Task<ActionResult<PatientErrorSettings>> SetPatientErrorSettings(PatientErrorSettings model)
    {
        _patientErrorSettings.Update(t =>
        {
            t.CreatePatientEnable = model.CreatePatientEnable;
            t.DeletePatientEnable = model.DeletePatientEnable;
            t.GetPatientEnable = model.GetPatientEnable;
            t.GetPatientsEnable = model.GetPatientsEnable;
            t.GetRandomPatientEnable = model.GetRandomPatientEnable;
            t.GetRandomPatientsEnable = model.GetRandomPatientsEnable;
            t.UpdatePatientEnable = model.UpdatePatientEnable;
        });
        return Ok(model);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("result")]
    public async Task<ActionResult<ResultsErrorSettings>> GetResultsErrorSettings()
    {
        return Ok(_examinationErrorSettings.Value);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("result")]
    public async Task<ActionResult<ResultsErrorSettings>> SetResultsErrorSettings(ResultsErrorSettings model)
    {
        _resultsErrorSettings.Update(t =>
        {
        });
        return Ok(model);
    }
}