namespace Arc.CustomApp.InfraTest;

public partial class ReadOnlyRepoTest
{
    [Fact]
    public async Task GetStudents_ReturnProperData()
    {
        // Arrange
        var expectedStudents = new List<Student>()
        {
            new(1, "S1", "Sub1"),
            new(2, "S2", "Sub2"),
            new(3, "S3", "Sub3"),
            new(4, "S4", "Sub4")
        };

        // Act
        var actualStudents = await _readOnlyRepo.GetStudents(_cancellationToken);

        // Assert
        actualStudents.ShouldBe(expectedStudents);
    }
}