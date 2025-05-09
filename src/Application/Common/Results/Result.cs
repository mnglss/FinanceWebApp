﻿namespace Application.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; }

        public Error Error { get; }

        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException("Success result cannot have an error message");
            if (!isSuccess && string.IsNullOrWhiteSpace(error.Message))
                throw new InvalidOperationException("Failure result must have an error message");
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);

        public static Result<T> Success<T>(T value) => new(value, true, Error.None);

        public static Result Failure(Error error) => new Result(false, error);

        public static Result<T> Failure<T>(Error error) => new(default!, false, error);
    }
}
