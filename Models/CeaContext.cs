using Microsoft.EntityFrameworkCore;  
namespace WebApi.Models 
{     
    public class CeaContext : DbContext     
  {         
        public CeaContext(DbContextOptions<CeaContext> options) : base(options)         
        {         
        }       
        public DbSet<Employees> Employees { get; set; } 
        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<HelpersModel> Helpers { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Shifts> Shifts { get; set; }
    } 
}