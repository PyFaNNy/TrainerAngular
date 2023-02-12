using App.Metrics;
using App.Metrics.Counter;

namespace Trainer.Metrics;

public class BusinessMetrics
{
    #region BaseUserController Metrics
    
    public static CounterOptions BaseUserGetModels => new CounterOptions
    {
        Context = "BaseUser Controller",
        Name = "Get Models request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"BaseUser"})
    };
    
    public static CounterOptions BaseUserBlockUser => new CounterOptions
    {
        Context = "BaseUser Controller",
        Name = "Block User request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"BaseUser"})
    };
    
    public static CounterOptions BaseUserUnBlockUser => new CounterOptions
    {
        Context = "BaseUser Controller",
        Name = "UnBlock User request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"BaseUser"})
    };
    
    public static CounterOptions BaseUserDeleteUser => new CounterOptions
    {
        Context = "BaseUser Controller",
        Name = "Delete User request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"BaseUser"})
    };
    
    public static CounterOptions BaseUserApproveUser => new CounterOptions
    {
        Context = "BaseUser Controller",
        Name = "Approve User request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"BaseUser"})
    };
    
    public static CounterOptions BaseUserDeclineUser => new CounterOptions
    {
        Context = "BaseUser Controller",
        Name = "Decline User request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"BaseUser"})
    };
    
    #endregion

    #region ExaminationController Metrics

    public static CounterOptions ExaminationGetModels => new CounterOptions
    {
        Context = "Examination Controller",
        Name = "Get Models request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"Examination"})
    };

    public static CounterOptions ExaminationGetModel => new CounterOptions
    {
        Context = "Examination Controller",
        Name = "Get Model request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"Examination"})
    };
    
    public static CounterOptions ExaminationAddModel => new CounterOptions
    {
        Context = "Examination Controller",
        Name = "Add Model request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"Examination"})
    };
    
    public static CounterOptions ExaminationUpdateModel => new CounterOptions
    {
        Context = "Examination Controller",
        Name = "Update Model request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"Examination"})
    };
    
    public static CounterOptions ExaminationDeleteModel => new CounterOptions
    {
        Context = "Examination Controller",
        Name = "Delete Model request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"Examination"})
    };
    
    #endregion
    
    #region PatientController Metrics

    public static CounterOptions PatientGetModels => new CounterOptions
    {
        Context = "Patient Controller",
        Name = "Get Models request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"Patient"})
    };

    public static CounterOptions PatientGetModel => new CounterOptions
    {
        Context = "Patient Controller",
        Name = "Get Model request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"Patient"})
    };
    
    public static CounterOptions PatientAddModel => new CounterOptions
    {
        Context = "Patient Controller",
        Name = "Add Model request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"Patient"})
    };
    
    public static CounterOptions PatientUpdateModel => new CounterOptions
    {
        Context = "Patient Controller",
        Name = "Update Model request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"Patient"})
    };
    
    public static CounterOptions PatientDeleteModel => new CounterOptions
    {
        Context = "Patient Controller",
        Name = "Delete Model request",
        MeasurementUnit = Unit.Calls,
        Tags = new MetricTags(new []{"Trainer"}, new []{"Patient"})
    };
    
    #endregion
}