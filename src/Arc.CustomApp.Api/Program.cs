using Arc.CustomApp.Api.Endpoints;
using Arc.CustomApp.Application;
using Arc.CustomApp.Infra;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Marker>());
builder.Services.AddTransient<IReadOnlyRepo, ReadOnlyRepo>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGetStudentEndpoint();
app.MapGetStudentByIdEndpoint();

app.Run();