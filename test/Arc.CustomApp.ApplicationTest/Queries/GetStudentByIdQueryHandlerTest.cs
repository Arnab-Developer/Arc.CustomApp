namespace Arc.CustomApp.ApplicationTest.Queries;

public partial class GetStudentByIdQueryHandlerTest
{
    private const int _studentId = 1;
    private readonly GetStudentByIdQuery _query = new(_studentId);
    private readonly Mock<IReadOnlyRepo> _repoMock = new();
    private readonly GetStudentByIdQueryHandler _handler;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public GetStudentByIdQueryHandlerTest() => _handler = new(_repoMock.Object);
}