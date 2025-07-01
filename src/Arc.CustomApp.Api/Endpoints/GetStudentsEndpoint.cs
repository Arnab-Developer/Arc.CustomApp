namespace Arc.CustomApp.Api.Endpoints;

internal static class GetStudentsEndpoint
{
    /// <summary>Maps the "get-students" endpoint to retrieve students data.</summary>
    /// <param name="builder">The endpoint builder to configure the endpoint.</param>
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