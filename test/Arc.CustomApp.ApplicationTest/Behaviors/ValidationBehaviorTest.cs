using FluentValidation;
using MediatR;

namespace Arc.CustomApp.ApplicationTest.Behaviors;

public partial class ValidationBehaviorTest
{
    private readonly Mock<IValidator<IRequest<bool>>> _validatorMock = new();
    private readonly IEnumerable<IValidator<IRequest<bool>>> _validators;
    private readonly ValidationBehavior<IRequest<bool>, bool> _validationBehavior;

    public ValidationBehaviorTest()
    {
        _validators = [_validatorMock.Object];
        _validationBehavior = new(_validators);
    }
}