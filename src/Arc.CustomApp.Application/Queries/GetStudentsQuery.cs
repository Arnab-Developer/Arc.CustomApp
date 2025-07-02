namespace Arc.CustomApp.Application.Queries;

/// <summary>A query to get the students data.</summary>
public record GetStudentsQuery : IRequest<GetStudentsQueryResponse>;

/// <summary>Validate the get students query.</summary>
public class GetStudentsQueryValidator : AbstractValidator<GetStudentsQuery>
{
    /// <summary>Creates a new instance of get students query validator.</summary>
    public GetStudentsQueryValidator() { }
}

/// <summary>A handler of the query which returns the students data.</summary>
/// <param name="repo">A read only repo.</param>
public class GetStudentsQueryHandler(IReadOnlyRepo repo)
    : IRequestHandler<GetStudentsQuery, GetStudentsQueryResponse>
{
    private readonly IReadOnlyRepo _repo = repo;

    /// <summary>A handler of the query which returns the students data.</summary>
    /// <param name="request">A query to get the students data.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the 
    /// operation before completion.</param>
    /// <returns>A task containing students data.</returns>
    public async Task<GetStudentsQueryResponse> Handle(
        GetStudentsQuery request,
        CancellationToken cancellationToken)
    {
        var students = await _repo.GetStudents().ConfigureAwait(false);

        var studentResponses = students.Select(s =>
            new GetStudentsQueryStudentResponse(s.Name, s.Subject));

        var getStudentsQueryResponse = new GetStudentsQueryResponse(studentResponses);
        return getStudentsQueryResponse;
    }
}

/// <summary>A query response for the students data.</summary>
/// <param name="Name">Name of the student.</param>
/// <param name="Subject">Subject of the student.</param>
public record GetStudentsQueryStudentResponse(string Name, string Subject);

/// <summary>A response of the query which have the students data.</summary>
/// <param name="Students">A collection of students.</param>
public record GetStudentsQueryResponse(IEnumerable<GetStudentsQueryStudentResponse> Students);