using Newtonsoft.Json;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.OTPCodes
{
    public abstract class RequestSmsCodeAbstractCommand
    {
        public string Email
        {
            get;
            set;
        }

        [JsonIgnore]
        public string Host
        {
            get;
            set;
        }

        [JsonIgnore]
        public OTPAction Action
        {
            get;
            set;
        }
    }
}
