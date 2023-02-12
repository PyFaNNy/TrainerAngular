﻿namespace Trainer.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string> Errors
        {
            get;
        }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string>();
        }

        public ValidationException(string propertyName, string errorMessage)
            : this()
        {
            Errors.Add(propertyName, errorMessage);
        }
    }
}
