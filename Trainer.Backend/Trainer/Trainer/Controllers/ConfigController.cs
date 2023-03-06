using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Controllers;

[ApiController]
[Route("config")]
[Authorize(Roles = "superAdmin")]
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
        return Ok(_patientErrorSettings.Value);
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
        return Ok(_resultsErrorSettings.Value);
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
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("otp")]
    public async Task<ActionResult<OTPCodesErrorSettings>> GetOTPCodesErrorSettings()
    {
        return Ok(_OTPCodesErrorSettings.Value);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("otp")]
    public async Task<ActionResult<OTPCodesErrorSettings>> SetOTPCodesErrorSettings(OTPCodesErrorSettings model)
    {
        _OTPCodesErrorSettings.Update(t =>
        {
            t.RequestLoginCodeEnable = model.RequestLoginCodeEnable;
            t.RequestRandomLoginCodeEnable = model.RequestRandomLoginCodeEnable;
            t.RequestPasswordEnable = model.RequestPasswordEnable;
            t.RequestRandomPasswordEnable = model.RequestRandomPasswordEnable;
            t.RequestRegistrationCodeEnable = model.RequestRegistrationCodeEnable;
            t.RequestRandomRegistrationCodeEnable = model.RequestRandomRegistrationCodeEnable;
            t.IsUniversalVerificationCodeEnabled = model.IsUniversalVerificationCodeEnabled;
            t.UniversalVerificationCode = model.UniversalVerificationCode;
        });
        return Ok(model);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("csv")]
    public async Task<ActionResult<CSVErrorSettings>> GetCSVErrorSettings()
    {
        return Ok(_CSVErrorSettings.Value);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("csv")]
    public async Task<ActionResult<CSVErrorSettings>> SetCSVErrorSettings(CSVErrorSettings model)
    {
        _CSVErrorSettings.Update(t =>
        {
            t.CSVToExaminationsEnable = model.CSVToExaminationsEnable;
            t.CSVToPatientsEnable = model.CSVToPatientsEnable;
        });
        return Ok(model);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("users")]
    public async Task<ActionResult<BaseUserErrorSettings>> GetBaseUserErrorSettings()
    {
        return Ok(_baseUserErrorSettings.Value);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("users")]
    public async Task<ActionResult<BaseUserErrorSettings>> SetBaseUserErrorSettings(BaseUserErrorSettings model)
    {
        _baseUserErrorSettings.Update(t =>
        {
            t.ApproveUserEnable = model.ApproveUserEnable;
            t.ApproveUserEmailEnable = model.ApproveUserEmailEnable;
            t.BlockUserEnable = model.BlockUserEnable;
            t.BlockUserEmailEnable = model.BlockUserEmailEnable;
            t.ChangeRoleEnable = model.ChangeRoleEnable;
            t.DeclineUserEnable = model.DeclineUserEnable;
            t.DeclineUserEmailEnable = model.DeclineUserEmailEnable;
            t.DeleteUserEnable = model.DeleteUserEnable;
            t.DeleteUserEmailEnable = model.DeleteUserEmailEnable;
            t.UnBlockUserEnable = model.UnBlockUserEnable;
            t.UnBlockUserEmailEnable = model.UnBlockUserEmailEnable;
            t.ResetPasswordUserEnable = model.ResetPasswordUserEnable;
            t.GetBaseUserEnable = model.GetBaseUserEnable;
            t.GetRandomBaseUserEnable = model.GetRandomBaseUserEnable;
            t.GetBaseUsersEnable = model.GetBaseUsersEnable;
            t.GetRandomBaseUsersEnable = model.GetRandomBaseUsersEnable;
            t.SignInEnable = model.SignInEnable;
        });
        return Ok(model);
    }
}