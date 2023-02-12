﻿using Trainer.Domain.Interfaces;
using Trainer.Enums;

namespace Trainer.Domain.Entities
{
    public class OTP : IBaseEntity
    {
        public Guid Id
        {
            get; set;
        }

        public string Value
        {
            get; set;
        }

        public bool IsValid
        {
            get; set;
        }

        public DateTime CreatedAt 
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public OTPAction Action
        {
            get; set;
        }
    }
}
