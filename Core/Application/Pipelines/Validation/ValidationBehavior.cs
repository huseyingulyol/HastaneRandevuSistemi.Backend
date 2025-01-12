﻿using FluentValidation;
using MediatR;
using ValidationException = Core.CrossCuttingConcerns.Exceptions.Types.ValidationException;

namespace Core.Application.Pipelines.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ValidationContext<object> context = new(request);

            var errors = _validators.Select(validator => validator.Validate(context))
                .SelectMany(result => result.Errors)
                .ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors.Select(e => e.ErrorMessage).ToList());
            }

            TResponse response = await next();
            return response;
        }
    }
}
