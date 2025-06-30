namespace Arc.CustomApp.Api.Endpoints;

/// <summary>
/// Provides functionality to map the "get-students" endpoint to retrieve student data.
/// </summary>
/// <remarks>This class defines the "get-students" endpoint, which handles HTTP GET requests to fetch a list of
/// students. The endpoint returns a list of students if available, or a "Not Found" response if no students are
/// found.</remarks>
internal static class GetStudentsEndpoint
{
    /// <summary>
    /// Maps the "get-students" endpoint to retrieve students data.
    /// </summary>
    /// <remarks>This method registers a GET endpoint at the route "get-students". The endpoint is designed to
    /// handle requests for retrieving students information.</remarks>
    /// <param name="builder">The <see cref="IEndpointRouteBuilder"/> used to configure the endpoint.</param>
    public static void MapGetStudentEndpoint(this IEndpointRouteBuilder builder) =>
        builder.MapGet("get-students", Handle);

    private static async Task<Results<Ok<GetStudentsEndpointResponse>, NotFound<GetStudentsEndpointError>>> Handle(
        IMediator mediator,
        CancellationToken token)
    {
        var query = new GetStudentsQuery();
        var queryResponse = await mediator.Send(query, token);

        if (!queryResponse.Students.Any())
        {
            var error = new GetStudentsEndpointError("Not found");
            return TypedResults.NotFound(error);
        }

        var students = queryResponse.Students
            .Select(s => new GetStudentsEndpointStudentResponse(s.Name, s.Subject))
            .ToList();

        var endpointResponse = new GetStudentsEndpointResponse(students);
        return TypedResults.Ok(endpointResponse);
    }

    private record GetStudentsEndpointStudentResponse(string Name, string Subject);

    private record GetStudentsEndpointResponse(IEnumerable<GetStudentsEndpointStudentResponse> Students);

    private record GetStudentsEndpointError(string Message);
}