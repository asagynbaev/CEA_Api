using Microsoft.EntityFrameworkCore;  
namespace WebApi.Models 
{     
    public class CeaContext : DbContext     
  {         
        public CeaContext(DbContextOptions<CeaContext> options) : base(options)         
        {         
        }       
        public DbSet<Employees> Employees { get; set; }     
    
    } 
}