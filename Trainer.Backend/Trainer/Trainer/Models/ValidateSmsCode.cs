using Trainer.Enums;

namespace Trainer.Models
{
    public class ValidateSmsCode
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public OTPAction OTPaction { get; set; }
    }
}
