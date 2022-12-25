using Newtonsoft.Json;

namespace Trainer.Infrastructure.Exceptions;

public class ApplicationValidationException
{
    [JsonProperty("errors")]
    public IDictionary<string, string> Failures
    {
        get;
    }
    
    public ApplicationValidationException(IDictionary<string, string> failures)
    {
        this.Failures = failures;
    }
}
