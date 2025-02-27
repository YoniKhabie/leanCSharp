using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;
// dotnet ef migrations add SeedGenres --output-dir Data/Migrations will create database from this class
public class GameStoreContext(DbContextOptions<GameStoreContext> options): DbContext(options)
{
    // Dbset will be a table in our database
    public DbSet<Game> Games => Set<Game>(); 

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // when migrartions start the function will be triggerd - OnModelCreating

        // adding dummies to the database
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name= "Fighting"},
            new {Id = 2, Name= "Roleplaying"},
            new {Id = 3, Name= "Sports"},
            new {Id = 4, Name= "Racing"},
            new {Id = 5, Name= "Kids and Family"}
        );
    }
}
