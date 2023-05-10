using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    public class eTicketContext:IdentityDbContext<ApplicationUser>
    {
        public eTicketContext(DbContextOptions<eTicketContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(a =>
                new { a.ActorId,a.MovieId}
            );
            modelBuilder.Entity<Actor_Movie>()
                .HasOne(a => a.Movie)
                .WithMany(a => a.Actors_Movies)
                .HasForeignKey(a => a.MovieId);
            modelBuilder.Entity<Actor_Movie>()
                .HasOne(a => a.Actor)
                .WithMany(a => a.Actors_Movies)
                .HasForeignKey(a => a.ActorId);
            
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<eTickets.Models.Actor> Actor { get; set; }
        public DbSet<eTickets.Models.Cinema> Cinema { get; set; }
        public DbSet<eTickets.Models.Producer> Producer { get; set; }
        public DbSet<eTickets.Models.Movie> Movie { get; set; }
        public DbSet<eTickets.Models.Actor_Movie> Actors_Movies { get; set; }
        public DbSet<eTickets.Models.Order> Order { get; set; }
        public DbSet<eTickets.Models.OrderItem> OrderItem { get; set; }
        public DbSet<eTickets.Models.ShoppingCartItem> ShoppingCartItem { get; set; }

    }
}
