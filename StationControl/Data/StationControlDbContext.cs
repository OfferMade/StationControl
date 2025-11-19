using Microsoft.EntityFrameworkCore;
using StationControl.Models.Entities;

namespace StationControl.Data
{
    public class StationControlDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KAGANERIS;initial catalog=StationDb;integrated security=true;TrustServerCertificate=True;");
        }

        public DbSet<Deputy> Deputies { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Mail> Mails { get; set; }
    }
}
