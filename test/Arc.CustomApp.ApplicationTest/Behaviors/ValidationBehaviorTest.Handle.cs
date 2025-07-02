namespace Arc.CustomApp.ApplicationTest.Behaviors;

public partial class ValidationBehaviorTest
{
    [Fact]
    public async Task Handle_DoNotFail_GivenSuccessValidationResult()
    {
        // Arrange
        _validatorMock
            .Setup(m => m.ValidateAsync(_requestMock.Object, _cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _nextMock
            .Setup(m => m(_cancellationToken))
            .ReturnsAsync(true);

        // Act
        var response = await _validationBehavior.Handle(
            _requestMock.Object, _nextMock.Object, _cancellationToken);

        // Assert
        _validatorMock.Verify(m => m.ValidateAsync(_requestMock.Object, _cancellationToken),
            Times.Once());

        _validatorMock.VerifyNoOtherCalls();

        _nextMock.Verify(m => m(_cancellationToken), Times.Once());
        _nextMock.VerifyNoOtherCalls();

        response.ShouldBe(true);
    }

    [Fact]
    public async Task Handle_Fail_GivenFailValidationResult()
    {
        // Arrange
        var validationFailure = new ValidationFailure("Id", "Id is null");

        _validatorMock
            .Setup(m => m.ValidateAsync(_requestMock.Object, _cancellationToken))
            .ReturnsAsync(new ValidationResult([validationFailure]));

        _nextMock
            .Setup(m => m(_cancellationToken))
            .ReturnsAsync(true);

        // Act
        Task<bool> func() => _validationBehavior.Handle(
            _requestMock.Object, _nextMock.Object, _cancellationToken);

        // Assert
        var exception = await func().ShouldThrowAsync<ValidationException>();
        exception.Message.ShouldBe("Id is null");

        _validatorMock.Verify(m => m.ValidateAsync(_requestMock.Object, _cancellationToken),
            Times.Once());

        _validatorMock.VerifyNoOtherCalls();

        _nextMock.Verify(m => m(_cancellationToken), Times.Never());
        _nextMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_CallNext_GivenEmptyValidators()
    {
        // Arrange
        ((List<IValidator<IRequest<bool>>>)_validators).Clear();

        _validatorMock
            .Setup(m => m.ValidateAsync(_requestMock.Object, _cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _nextMock
            .Setup(m => m(_cancellationToken))
            .ReturnsAsync(true);

        // Act
        var response = await _validationBehavior.Handle(
            _requestMock.Object, _nextMock.Object, _cancellationToken);

        // Assert
        _validatorMock.Verify(m => m.ValidateAsync(_requestMock.Object, _cancellationToken),
            Times.Never());

        _validatorMock.VerifyNoOtherCalls();

        _nextMock.Verify(m => m(_cancellationToken), Times.Once());
        _nextMock.VerifyNoOtherCalls();

        response.ShouldBe(true);
    }
}