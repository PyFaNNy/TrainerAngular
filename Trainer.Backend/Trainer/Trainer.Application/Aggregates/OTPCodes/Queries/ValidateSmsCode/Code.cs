namespace Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode
{
    public class Code
    {
        public string CodeValue
        {
            get; set;
        }

        public bool IsValid
        {
            get; set;
        }
    }
}
