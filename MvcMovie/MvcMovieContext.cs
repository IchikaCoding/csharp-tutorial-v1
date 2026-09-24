using Microsoft.EntityFrameworkCore;

public class MvcMovieContext(DbContextOptions<MvcMovieContext> options) : DbContext(options)
{
    public DbSet<MvcMovie.Models.Movie> Movie { get; set; } = default!;
    // TODO: このdefault!ってなんだろう？
    public DbSet<MvcMovie.Models.Resident> Resident { get; set; } = default!;
}
