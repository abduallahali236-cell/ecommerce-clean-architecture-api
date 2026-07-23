using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Models
{
    public sealed class Result<T> : IResult
    {
        private Result(
            T? value,
            bool isSuccess,
            IReadOnlyList<Error> errors)
        {
            Value = value;
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public T? Value { get; }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors { get; }

        public static Result<T> Success(T value)
            => new(value, true, []);

        public static Result<T> Failure(params Error[] errors)
            => new(default, false, errors);

        public static Result<T> Failure(IEnumerable<Error> errors)
            => new(default, false, errors.ToList());
    }
}
