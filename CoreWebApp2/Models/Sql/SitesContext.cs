using Microsoft.EntityFrameworkCore;

namespace CoreWebApp2.Models.Sql
{
    public class SitesContext: DbContext
    {
        public SitesContext(DbContextOptions<SitesContext> options)
            : base(options)
        {

        }
        public DbSet<SitesDB> Sites { get; set; }
        public DbSet<SearchedItem> Records { get; set; }
    }
}
