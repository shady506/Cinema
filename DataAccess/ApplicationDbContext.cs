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
        public DbSet<UserOTP> UserOTPs{ get; set; }
        public DbSet<Ticket> Tickets{ get; set; }
        public DbSet<Promotion> Promotions { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Data Source=MSI\\SHADY;Initial Catalog=Cinema;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
        }





        //public ApplicationDbContext()
        //{

        //}
    }
}
