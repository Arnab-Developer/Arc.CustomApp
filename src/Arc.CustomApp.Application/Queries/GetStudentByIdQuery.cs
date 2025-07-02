namespace Arc.CustomApp.Application.Queries;

/// <summary>A query to get the student data based on student id.</summary>
/// <param name="Id">The id of the student.</param>
public record GetStudentByIdQuery(int Id) : IRequest<GetStudentByIdQueryResponse>;

/// <summary>Validate the get student by id query.</summary>
public class GetStudentByIdQueryValidator : AbstractValidator<GetStudentByIdQuery>
{
    /// <summary>Creates a new instance of get student by id query validator.</summary>
    public GetStudentByIdQueryValidator() => RuleFor(r => r.Id).GreaterThan(0);
}

/// <summary>A handler of the query which returns the student data by id.</summary>
/// <param name="repo">A read only repo.</param>
public class GetStudentByIdQueryHandler(IReadOnlyRepo repo)
    : IRequestHandler<GetStudentByIdQuery, GetStudentByIdQueryResponse>
{
    private readonly IReadOnlyRepo _repo = repo;

    /// <summary>A handler of the query which returns the student data by id.</summary>
    /// <param name="request">A query to get the student data.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the 
    /// operation before completion.</param>
    /// <returns>A task containing student data.</returns>
    public async Task<GetStudentByIdQueryResponse> Handle(
        GetStudentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var student = await _repo.GetStudentById(request.Id).ConfigureAwait(false);
        var response = new GetStudentByIdQueryResponse(student.Name, student.Subject);
        return response;
    }
}

/// <summary>A query response for the student data.</summary>
/// <param name="Name">The name of the student.</param>
/// <param name="Subject">The subject of the student.</param>
public record GetStudentByIdQueryResponse(string Name, string Subject);