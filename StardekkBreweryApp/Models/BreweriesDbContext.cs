using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StardekkBreweryApp.Models
{
    public partial class BreweriesDbContext : DbContext
    {
        public BreweriesDbContext()
        {
        }

        public BreweriesDbContext(DbContextOptions<BreweriesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brewery> Brewery { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Users\\Jeroen\\source\\repos\\StardekkBreweryApp\\StardekkBreweryApp\\App_Data\\Breweries.mdf;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brewery>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}
