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
}