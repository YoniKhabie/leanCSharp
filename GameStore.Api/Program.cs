// fn + f5 = debug
// dotnet run = run
// dotnet watch run = run with watch changes

using GameStore.Api.Dtos;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGameEndpoints();

app.Run();
