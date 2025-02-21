﻿namespace DVLD.Application.Common.Errors
{
    public class Error
    {
        // Common Errors that could occuer inside API
        private static readonly string _recoredNotFound = "recoredNotFound";
        private static readonly string _validationError = "ValidationError";


        private readonly string _code;
        private readonly string _message;

        public string Message { get { return _message; } }
        public string Code { get { return _code; } }




        public Error(string code, string message)
        {
            _code = code;
            _message = message;
        }

        public static Error None => new(string.Empty, string.Empty);

        public static Error RecoredNotFound(string message) => new(_recoredNotFound, message);

        public static Error ValidationError(string message) => new(_validationError, message);

    }
}
