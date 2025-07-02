namespace Arc.CustomApp.Application.Behaviors;

/// <summary>A validation behavior which validates the mediator query or commands
/// in the mediator pipeline.</summary>
/// <typeparam name="TRequest">The mediator query or command.</typeparam>
/// <typeparam name="TResponse">The response of the mediator query or command.</typeparam>
/// <param name="validators">The validators with which the validation needs to be performed.</param>
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    /// <summary>Validates the mediator query or commands.</summary>
    /// <param name="request">The mediator query or command.</param>
    /// <param name="next">The next behavior in the pipeline.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the 
    /// operation before completion.</param>
    /// <returns>A task containing the response of the mediator query or command.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            foreach (var validator in _validators)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken)
                    .ConfigureAwait(false);

                if (validationResult.IsValid)
                {
                    continue;
                }

                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage)
                    .Aggregate((a, b) => $"{a}, {b}");

                throw new ValidationException(errorMessages);
            }
        }

        return await next(cancellationToken).ConfigureAwait(false);
    }
}