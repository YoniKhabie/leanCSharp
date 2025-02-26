using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options): DbContext(options)
{
    public DbSet<Game> Game => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();
}
