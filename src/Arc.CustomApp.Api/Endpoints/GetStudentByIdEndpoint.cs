namespace Arc.CustomApp.Api.Endpoints;

internal static class GetStudentByIdEndpoint
{
    /// <summary>Maps the "get-student-by-id" endpoint to retrieve student data.</summary>
    /// <param name="builder">The endpoint builder to configure the endpoint.</param>
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