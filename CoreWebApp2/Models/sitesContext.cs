using Microsoft.EntityFrameworkCore;

namespace CoreWebApp2.Models
{
    public class SitesContext : DbContext
    {
        public SitesContext(): base()
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
        public DbSet<Sites> Sites { get; set; }
    }
}
