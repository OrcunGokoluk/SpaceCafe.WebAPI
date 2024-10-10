using FluentValidation;
using MediatR;
using SpaceCafe.Application.Common.CustomExceptions;

namespace SpaceCafe.Application.Common.Behaviours;

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
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Any())
            {
                var errorDetails = failures.ToDictionary(
                    failure => failure.PropertyName,
                    failure => failure.ErrorMessage);

                throw new CustomValidationException(errorDetails);
            }
        }

        return await next();
    }
}








/*public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>?> _validator;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>?> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {




        if (!_validator.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errorDictionary = _validator
            .Select(s => s.Validate(context))
            .SelectMany(s => s.Errors)
            .Where(s => s != null)
            .GroupBy(
            s => s.PropertyName,
            s => s.ErrorMessage, (propertyName, errorMessage) => new
            {
                Key = propertyName,
                Value = errorMessage.Distinct().ToArray()
            })
            .ToDictionary(s => s.Key, s => s.Value[0]);

        if (errorDictionary.Any())
        {
            var errors = errorDictionary.Select(s => new ValidationFailure
            {
                PropertyName = s.Value,
                ErrorCode = s.Key
            });

            throw new ValidationException(errors);
        }
        return await next();

     }
}*/



/*

  if (_validator.Any())
  {
      var context = new ValidationContext<TRequest>(request);
      var validationResults = await Task.WhenAll(_validator.Select(v => v.ValidateAsync(context, cancellationToken)));
      var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

      if (failures.Any())
      {
          throw new ValidationException(failures);
      }
  }

  return await next();*/








/* if (_validator is null)
 {
     return await next();
 }

 var validationResult = await _validator.ValidateAsync(request, cancellationToken);

 if (validationResult.IsValid)
 {
     return await next();
 }

 var errors = validationResult.Errors
     .ConvertAll(ValidationFailure => Error.Validation(
         ValidationFailure.PropertyName,
         ValidationFailure.ErrorMessage));

 return errors;*/

