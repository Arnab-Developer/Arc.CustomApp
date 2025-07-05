namespace Arc.CustomApp.InfraTest;

public partial class ReadOnlyRepoTest
{
    private readonly ReadOnlyRepo _readOnlyRepo = new();
    private readonly CancellationToken _cancellationToken = CancellationToken.None;
}