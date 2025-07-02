namespace Arc.CustomApp.ApplicationTest.Queries;

public partial class GetStudentsQueryHandlerTest
{
    [Fact]
    public async Task Handle_ReturnValidStudents()
    {
        // Arrange
        _repoMock
            .Setup(m => m.GetStudents(_cancellationToken))
            .ReturnsAsync(
            [
                new(1, "S1", "Sub1"),
                new(2, "S2", "Sub2"),
                new(3, "S3", "Sub3"),
                new(4, "S4", "Sub4")
            ]);

        var expectedStudents = new List<GetStudentsQueryStudentResponse>()
        {
            new("S1", "Sub1"),
            new("S2", "Sub2"),
            new("S3", "Sub3"),
            new("S4", "Sub4")
        };

        var expectedResponse = new GetStudentsQueryResponse(expectedStudents);

        // Act
        var actualResponse = await _handler.Handle(_query, _cancellationToken);

        // Assert
        actualResponse.Students.Count().ShouldBe(4);
        actualResponse.Students.ShouldBe(expectedResponse.Students);

        _repoMock.Verify(m => m.GetStudents(_cancellationToken), Times.Once());
        _repoMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_ThrowsException_GivenExceptionInMock()
    {
        // Arrange
        _repoMock
            .Setup(m => m.GetStudents(_cancellationToken))
            .ThrowsAsync(new Exception("Data not found"));

        // Act
        Task<GetStudentsQueryResponse> func() => _handler.Handle(_query, _cancellationToken);

        // Assert
        var exception = await func().ShouldThrowAsync<Exception>();
        exception.Message.ShouldBe("Data not found");
    }
}