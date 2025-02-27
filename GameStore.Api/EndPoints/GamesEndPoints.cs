

using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;

namespace GameStore.Api.EndPoints;

public static class GamesEndPoints
{
    private static readonly List<GameDto> games = [
        new(1, "The Witcher 3: Wild Hunt", "RPG", 39.99m, new DateOnly(2015, 5, 19)),
        new(2, "Cyberpunk 2077", "Action RPG", 59.99m, new DateOnly(2020, 12, 10)),
        new(3, "Halo Infinite", "FPS", 49.99m, new DateOnly(2021, 12, 8)),
        new(4, "God of War", "Action", 29.99m, new DateOnly(2018, 4, 20)),
        new(5, "Red Dead Redemption 2", "Adventure", 44.99m, new DateOnly(2018, 10, 26)),
        new(6, "Elden Ring", "Action RPG", 59.99m, new DateOnly(2022, 2, 25)),
        new(7, "Super Mario Odyssey", "Platformer", 49.99m, new DateOnly(2017, 10, 27)),
        new(8, "The Legend of Zelda: Breath of the Wild", "Adventure", 59.99m, new DateOnly(2017, 3, 3)),
        new(9, "Minecraft", "Sandbox", 29.99m, new DateOnly(2011, 11, 18)),
        new(10, "Dark Souls III", "RPG", 39.99m, new DateOnly(2016, 4, 12))
    ];

    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app){
        var group = app.MapGroup("/games").WithParameterValidation();

        group.MapGet("", () => games);

        group.MapGet("/{id}", (int id) => {
            GameDto? game = games.Find(game => game.Id == id);
            if(game is null){
                return Results.NotFound();
            }
            return Results.Ok(game);
        });

        group.MapPost("", (CreateGameDto newGame, GameStoreContext dbContext) =>{
            //old approach
            // GameDto game = new(
            //     games.Count+1,
            //     newGame.Name,
            //     newGame.Genre,
            //     newGame.Price,
            //     newGame.ReleaseDate);
            // games.Add(game);
             

            //new approach
            // Game game = new(){
            //     Name = newGame.Name,
            //     Genre = dbContext.Genres.Find(newGame.GenreId),
            //     GenreId = newGame.GenreId,
            //     Price = newGame.Price,
            //     ReleasDate = newGame.ReleaseDate
            // };

            // GameDto gameDto = new(
            //     game.Id,
            //     game.Name,
            //     game.Genre!.Name,
            //     game.Price,
            //     game.ReleasDate
            // );


            // optimized new approach
            Game game = newGame.ToEntity();
            game.Genre = dbContext.Genres.Find(newGame.GenreId);
            dbContext.Games.Add(game);
            dbContext.SaveChanges(); // ID is generated here
            Console.WriteLine(game.Id);
            GameDto gameDto = game.ToDto();
            Console.WriteLine(gameDto.Id);



            return Results.Ok(gameDto);
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame)=>{
            var index = games.FindIndex(game => game.Id ==id);
            if(index == -1){
                return Results.NotFound();
            }
            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.Ok(games);
        });

        group.MapDelete("/{id}", (int id)=>{
            games.RemoveAll(game => game.Id == id);
            return Results.Ok(games);
        });

        return group;
    }
}
