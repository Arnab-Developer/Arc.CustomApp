namespace Arc.CustomApp.Application.Queries;

/// <summary>
/// Represents a query to retrieve a list of students.
/// </summary>
/// <remarks>This query is used to request student data.</remarks>
public record GetStudentsQuery : IRequest<GetStudentsQueryResponse>;

/// <summary>
/// Handles the query to retrieve a list of students and their associated subjects.
/// </summary>
/// <remarks>This class is responsible for processing the <see cref="GetStudentsQuery"/> and returning a  <see
/// cref="GetStudentsQueryResponse"/> containing the requested student data. It interacts  with the repository to fetch
/// student information and maps the data into the appropriate response format.</remarks>
/// <param name="repo">A read only repo.</param>
public class GetStudentsQueryHandler(IReadOnlyRepo repo)
    : IRequestHandler<GetStudentsQuery, GetStudentsQueryResponse>
{
    private readonly IReadOnlyRepo _repo = repo;

    /// <summary>
    /// Handles the query to retrieve a list of students and their associated subjects.
    /// </summary>
    /// <remarks>This method retrieves all students from the repository and maps their data 
    /// into a response format.</remarks>
    /// <param name="request">The query containing the parameters for retrieving students.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation before 
    /// completion.</param>
    /// <returns>A <see cref="GetStudentsQueryResponse"/> containing the list of students and 
    /// their subjects.</returns>
    public async Task<GetStudentsQueryResponse> Handle(
        GetStudentsQuery request,
        CancellationToken cancellationToken)
    {
        var students = await _repo.GetStudents();

        var studentResponses = students.Select(s =>
            new GetStudentsQueryStudentResponse(s.Name, s.Subject));

        var getStudentsQueryResponse = new GetStudentsQueryResponse(studentResponses);
        return getStudentsQueryResponse;
    }
}

/// <summary>
/// Represents the response for a query to retrieve student information.
/// </summary>
/// <remarks>This record encapsulates the name of a student and their associated subject. It is typically 
/// used as part of a query result in scenarios where student data needs to be retrieved.</remarks>
/// <param name="Name">Name of the student.</param>
/// <param name="Subject">Subject of the student.</param>
public record GetStudentsQueryStudentResponse(string Name, string Subject);

/// <summary>
/// Represents the response for a query to retrieve a collection of students.
/// </summary>
/// <remarks>This record encapsulates the result of a query operation, providing a collection of student 
/// details. Each student is represented by an instance of <see cref="GetStudentsQueryStudentResponse"/>.</remarks>
/// <param name="Students">A collection of <see cref="GetStudentsQueryStudentResponse"/> objects representing the students 
/// retrieved by the query.</param>
public record GetStudentsQueryResponse(IEnumerable<GetStudentsQueryStudentResponse> Students);