// fn + f5 = debug
// dotnet run = run
// dotnet watch run = run with watch changes

using GameStore.Api.Data;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);
builder.Services.AddScoped<GameStoreContext>();
var app = builder.Build();

app.MapGameEndpoints();

await app.MigrateDbAsync();

app.Run();
