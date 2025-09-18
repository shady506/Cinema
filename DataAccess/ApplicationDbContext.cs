using Cinema.DataAccess.Enums;
using Cinema.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cinema.ViewModels;

namespace Cinema.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) 
        {
            
        }

        public DbSet<Actors> Actors { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Movies> Movies{ get; set; }
        public DbSet<Cinemas> Cinemas{ get; set; }
        public DbSet<CartItem> cartItems { get; set; }
        public DbSet<ShowTime> showTimes { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Seat>  seats{ get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Data Source=MSI\\SHADY;Initial Catalog=Cinema;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Booking → ShowTime
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ShowTime)
                .WithMany()
                .HasForeignKey(b => b.ShowTimeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking → Seat
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Seat)
                .WithMany()
                .HasForeignKey(b => b.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            // CartItem → ShowTime
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.ShowTime)
                .WithMany()
                .HasForeignKey(c => c.ShowTimeId)
                .OnDelete(DeleteBehavior.Restrict);

            // CartItem → Seat
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Seat)
                .WithMany()
                .HasForeignKey(c => c.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            var seats = new List<Seat>();
            int seatId = 1;

            for (int movieId = 1; movieId <= 5; movieId++) // على حسب عدد الأفلام اللي عندك
            {
                for (int i = 1; i <= 10; i++)
                {
                    seats.Add(new Seat
                    {
                        Id = seatId++,
                        SeatNumber = $"A{i}",
                        IsBooked = false,
                        MovieId = movieId
                    });
                }
            }

            modelBuilder.Entity<Seat>().HasData(seats);
        }



        //public ApplicationDbContext()
        //{

        //}
    }
}
