namespace Arc.CustomApp.Application.Queries;

/// <summary>
/// Represents a query to retrieve a student by their unique identifier.
/// </summary>
/// <remarks>This query is used to request information about a specific student based on their ID. It is typically
/// handled by a mediator or query handler that processes the request and returns the corresponding <see
/// cref="GetStudentByIdQueryResponse"/>.</remarks>
/// <param name="Id">The unique identifier of the student to retrieve. Must be a positive integer.</param>
public record GetStudentByIdQuery(int Id) : IRequest<GetStudentByIdQueryResponse>;

/// <summary>
/// Handles the retrieval of a student's information by their unique identifier.
/// </summary>
/// <remarks>This class processes a <see cref="GetStudentByIdQuery"/> request and returns a <see
/// cref="GetStudentByIdQueryResponse"/> containing the student's name and subject. It uses an <see
/// cref="IReadOnlyRepo"/> to fetch the student data.</remarks>
/// <param name="repo">A read only repo.</param>
public class GetStudentByIdQueryHandler(IReadOnlyRepo repo)
    : IRequestHandler<GetStudentByIdQuery, GetStudentByIdQueryResponse>
{
    private readonly IReadOnlyRepo _repo = repo;

    /// <summary>
    /// Handles the query to retrieve a student's details by their unique identifier.
    /// </summary>
    /// <remarks>This method retrieves the student's details from the repository based on the provided
    /// identifier. Ensure that the <paramref name="request"/> contains a valid student ID.</remarks>
    /// <param name="request">The query containing the student's unique identifier.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation before it completes.</param>
    /// <returns>A <see cref="GetStudentByIdQueryResponse"/> containing the student's name and subject.</returns>
    public async Task<GetStudentByIdQueryResponse> Handle(
        GetStudentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var student = await _repo.GetStudentById(request.Id);
        var response = new GetStudentByIdQueryResponse(student.Name, student.Subject);
        return response;
    }
}

/// <summary>
/// Represents the response for a query to retrieve a student's details by their ID.
/// </summary>
/// <remarks>This record encapsulates the student's name and subject as part of the query result.</remarks>
/// <param name="Name">The name of the student.</param>
/// <param name="Subject">The subject associated with the student.</param>
public record GetStudentByIdQueryResponse(string Name, string Subject);