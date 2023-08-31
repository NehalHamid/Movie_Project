using Microsoft.EntityFrameworkCore;
using Movie_Project.Models;

namespace Movie_Project.Data
{
    public class MovieDBContext:DbContext
    {
        public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options)
        {
        }
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Movie>()
                        .HasOne(b => b.movie)
                        .WithMany(ba => ba.user_Movies)
                        .HasForeignKey(bi => bi.userId);


            //modelBuilder.Entity<User_Movie>()
            //            .HasOne(b => b.actor)
            //            .WithMany(ba => ba.user_Movies)
            //            .HasForeignKey(bi => bi.userId);
        }
        protected MovieDBContext()
        {

        }
        public DbSet<Gener> Geners { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie_Project.Models.Login>? Login { get; set; }
    }
}
