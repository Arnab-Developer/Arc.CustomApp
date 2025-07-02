namespace Arc.CustomApp.ApplicationTest.Queries;

public partial class GetStudentsQueryHandlerTest
{
    private readonly GetStudentsQuery _query = new();
    private readonly Mock<IReadOnlyRepo> _repoMock = new();
    private readonly GetStudentsQueryHandler _handler;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public GetStudentsQueryHandlerTest() => _handler = new(_repoMock.Object);
}