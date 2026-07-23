using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Models
{
    public sealed class Result : IResult
    {
        private Result(
            bool isSuccess,
            IReadOnlyList<Error> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors { get; }

        public static Result Success()
            => new(true, []);

        public static Result Failure(params Error[] errors)
            => new(false, errors);

        public static Result Failure(IEnumerable<Error> errors)
            => new(false, errors.ToList());
    }
}
