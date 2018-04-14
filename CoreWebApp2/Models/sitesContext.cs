using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp2.Models
{
    public class sitesContext : DbContext
    {
        public sitesContext(): base()
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
        public DbSet<sites> Sites { get; set; }
    }
}
