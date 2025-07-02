namespace Arc.CustomApp.ApplicationTest.Queries;

public partial class GetStudentByIdQueryHandlerTest
{
    [Fact]
    public async Task Handle_ReturnValidStudent_GivenValidInput()
    {
        // Arrange
        _repoMock
            .Setup(m => m.GetStudentById(_studentId, _cancellationToken))
            .ReturnsAsync(new Student(1, "s3", "Sub3"));

        var expectedResponse = new GetStudentByIdQueryResponse("s3", "Sub3");

        // Act
        var actualResponse = await _handler.Handle(_query, _cancellationToken);

        // Assert
        actualResponse.ShouldBe(expectedResponse);

        _repoMock.Verify(m => m.GetStudentById(_studentId, _cancellationToken), Times.Once());
        _repoMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_ThrowsException_GivenExceptionInMock()
    {
        // Arrange
        _repoMock
            .Setup(m => m.GetStudentById(_studentId, _cancellationToken))
            .ThrowsAsync(new Exception("Data not found"));

        // Act
        Task<GetStudentByIdQueryResponse> func() => _handler.Handle(_query, _cancellationToken);

        // Assert
        var exception = await func().ShouldThrowAsync<Exception>();
        exception.Message.ShouldBe("Data not found");
    }
}