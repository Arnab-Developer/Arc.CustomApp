using FluentValidation.Results;

namespace Arc.CustomApp.ApplicationTest.Behaviors;

public partial class ValidationBehaviorTest
{
    [Fact]
    public async Task Handle_DoNotFail_GivenValidId()
    {
        // Arrange
        _validatorMock
            .Setup(m => m.ValidateAsync(_requestMock.Object, _cancellationToken))
            .ReturnsAsync(new ValidationResult());

        // Act
        var response = await _validationBehavior.Handle(
            _requestMock.Object, _nextMock.Object, _cancellationToken);

        // Assert
        _validatorMock.Verify(m => m.ValidateAsync(_requestMock.Object, _cancellationToken), 
            Times.Once());

        _validatorMock.VerifyNoOtherCalls();

        _nextMock.Verify(m => m(_cancellationToken), Times.Once());
        _nextMock.VerifyNoOtherCalls();
    }
}