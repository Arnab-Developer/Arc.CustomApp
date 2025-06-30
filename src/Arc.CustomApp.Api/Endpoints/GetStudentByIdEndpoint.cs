namespace Arc.CustomApp.Api.Endpoints;

/// <summary>
/// Provides functionality to map the "get-student-by-id" endpoint, which retrieves student details by their unique
/// identifier.
/// </summary>
/// <remarks>This endpoint is designed to handle HTTP GET requests to the "get-student-by-id" route. It processes
/// the request by querying for a student based on the provided ID and returns either the student's details or an error
/// if the student is not found.</remarks>
internal static class GetStudentByIdEndpoint
{
    /// <summary>
    /// Maps an HTTP GET endpoint to retrieve a student by their unique identifier.
    /// </summary>
    /// <remarks>The endpoint is registered at the route "get-student-by-id". It expects a query parameter 
    /// specifying the student's ID.</remarks>
    /// <param name="builder">The <see cref="IEndpointRouteBuilder"/> used to configure the endpoint.</param>
    public static void MapGetStudentByIdEndpoint(this IEndpointRouteBuilder builder) =>
        builder.MapGet("get-student-by-id", Handle);

    private static async Task<Results<Ok<GetStudentByIdEndpointResponse>, NotFound<GetStudentByIdEndpointError>>> Handle(
        int id,
        IMediator mediator,
        CancellationToken token)
    {
        var query = new GetStudentByIdQuery(id);
        var queryResponse = await mediator.Send(query, token);

        if (string.IsNullOrEmpty(queryResponse.Name) ||
            string.IsNullOrEmpty(queryResponse.Subject))
        {
            var error = new GetStudentByIdEndpointError("Not found");
            return TypedResults.NotFound(error);
        }

        var endpointResponse =
            new GetStudentByIdEndpointResponse(queryResponse.Name, queryResponse.Subject);

        return TypedResults.Ok(endpointResponse);
    }

    private record GetStudentByIdEndpointResponse(string Name, string Subject);

    private record GetStudentByIdEndpointError(string Message);
}