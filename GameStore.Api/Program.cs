// fn + f5 = debug
// dotnet run = run
// dotnet watch run = run with watch changes

using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGameEndpoints();

app.Run();
