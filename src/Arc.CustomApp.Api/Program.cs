using Arc.CustomApp.Api.Endpoints;
using Arc.CustomApp.Api.Middlewares;
using Arc.CustomApp.Application;
using Arc.CustomApp.Application.Behaviors;
using Arc.CustomApp.Infra;
using FluentValidation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddTransient<IReadOnlyRepo, ReadOnlyRepo>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddTransient<IValidator<GetStudentByIdQuery>, GetStudentByIdQueryValidator>();
builder.Services.AddTransient<IValidator<GetStudentsQuery>, GetStudentsQueryValidator>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Marker>();
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();

app.MapGetStudentEndpoint();
app.MapGetStudentByIdEndpoint();

app.Run();