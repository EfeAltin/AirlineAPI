namespace AirlineAPI.Data
{
    using AirlineAPI.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Net.Sockets;

    public class AppDbContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=database;Integrated Security=True;Trust Server Certificate=True");
        }





    }
}
