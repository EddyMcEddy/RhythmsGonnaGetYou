using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    public class AlbumContext : DbContext
    {

        public DbSet<Albums> Album { get; set; }
        public DbSet<Bands> Band { get; set; }
        public DbSet<Songs> Song { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=RhythmsGonnaGetYou");
        }
    }
}