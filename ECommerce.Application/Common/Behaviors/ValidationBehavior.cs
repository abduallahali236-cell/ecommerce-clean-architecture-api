using System;
using System.Collections.Generic;
using System.Text;
    using FluentValidation;
    using MediatR;

namespace ECommerce.Application.Common.Behaviors
{

    public sealed class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var errors = validationResults
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .Select(x => new Error(
                    x.ErrorCode,
                    x.ErrorMessage,
                    ErrorType.Validation))
                .ToList();

            if (errors.Count == 0)
                return await next();

            return CreateValidationResult(errors);
        }

        private static TResponse CreateValidationResult(
            IReadOnlyList<Error> errors)
        {
            var responseType = typeof(TResponse);

            // Result
            if (responseType == typeof(Result))
            {
                return (TResponse)(object)Result.Failure(errors);
            }

            // Result<T>
            if (responseType.IsGenericType &&
                responseType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var valueType = responseType.GetGenericArguments()[0];

                var failureMethod = responseType.GetMethod(
                    nameof(Result<object>.Failure),
                    new[] { typeof(IEnumerable<Error>) });

                if (failureMethod is null)
                    throw new InvalidOperationException(
                        $"Failure factory not found for {responseType.Name}.");

                return (TResponse)failureMethod.Invoke(null, new object[] { errors })!;
            }

            throw new InvalidOperationException(
                $"ValidationBehavior only supports Result or Result<T>. " +
                $"Current response type: {responseType.Name}");
        }
    }
}
