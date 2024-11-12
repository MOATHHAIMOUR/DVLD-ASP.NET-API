using DVLD.Application.Common.Errors;

namespace DVLD.Application.Common.ResultPattern
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public Error Error { get; }

        private Result(Error error)
        {
            Value = default;
            Error = error;
            IsSuccess = false;
        }

        private Result(T? value)
        {
            Value = value;
            Error = Error.None;
            IsSuccess = true;
        }

        public static Result<T> Success(T value) => new(value);

        public static Result<T> Failure(Error error) => new(error);
    }
}
