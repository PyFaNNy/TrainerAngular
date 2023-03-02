using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Trainer.Application.Aggregates.Examination.Queries.GetExamination;
using Trainer.Metrics;
using Trainer.Settings.Error;

namespace Trainer.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigController : BaseController
{
    private readonly ExaminationErrorSettings _examinationErrorSettings;

    public ConfigController(ILogger<AccountController> logger,
        IOptions<ExaminationErrorSettings> examinationErrorSettings) : base(logger)
    {
        _examinationErrorSettings = examinationErrorSettings.Value;
    }
    
    [HttpGet]
    public async Task<ActionResult<ExaminationErrorSettings>> GetExaminationErrorSettings()
    {
        return Ok(_examinationErrorSettings);
    }
    
    [HttpPost]
    public async Task<ActionResult<ExaminationErrorSettings>> SetExaminationErrorSettings(ExaminationErrorSettings model)
    {
        return Ok(model);
    }
}