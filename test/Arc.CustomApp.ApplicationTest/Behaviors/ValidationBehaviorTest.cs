namespace Arc.CustomApp.ApplicationTest.Behaviors;

public partial class ValidationBehaviorTest
{
    private readonly Mock<IRequest<bool>> _requestMock = new();
    private readonly Mock<IValidator<IRequest<bool>>> _validatorMock = new();
    private readonly IEnumerable<IValidator<IRequest<bool>>> _validators;
    private readonly ValidationBehavior<IRequest<bool>, bool> _validationBehavior;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;
    private readonly Mock<RequestHandlerDelegate<bool>> _nextMock = new();

    public ValidationBehaviorTest()
    {
        _validators = new List<IValidator<IRequest<bool>>> { _validatorMock.Object };
        _validationBehavior = new(_validators);
    }
}