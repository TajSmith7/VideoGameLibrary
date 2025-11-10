using Microsoft.EntityFrameworkCore;
using VideoGameLibrary.Models;

namespace VideoGameLibrary.Data
{
    public class VideoGameLibraryContext : DbContext
    {
        public VideoGameLibraryContext(DbContextOptions<VideoGameLibraryContext> options)
            : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameGenre>()
                .HasKey(gg => new { gg.GameId, gg.GenreId });

            modelBuilder.Entity<GamePlatform>()
                .HasKey(gp => new { gp.GameId, gp.PlatformId });
        }
    }
}
