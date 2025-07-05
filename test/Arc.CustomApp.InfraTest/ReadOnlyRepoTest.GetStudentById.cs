namespace Arc.CustomApp.InfraTest;

public partial class ReadOnlyRepoTest
{
    [Fact]
    public async Task GetStudentById_ReturnProperData_GivenValidId()
    {
        // Arrange
        var expectedStuent = new Student(105, "105 s3", "Sub3");

        // Act
        var actualStudent = await _readOnlyRepo.GetStudentById(105, _cancellationToken);

        // Assert
        actualStudent.ShouldBe(expectedStuent);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetStudentById_ThrowsException_GivenInvalidId(int id)
    {
        // Act
        Task<Student> func() => _readOnlyRepo.GetStudentById(id, _cancellationToken);

        // Assert
        var exception = await func().ShouldThrowAsync<ArgumentException>();
        exception.Message.ShouldBe("Required input id cannot be zero or negative. (Parameter 'id')");
    }
}