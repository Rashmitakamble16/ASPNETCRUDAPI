using CrudAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Data
{
    public class CrudAPIDbContext : DbContext
    {
        public CrudAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Crud> Contacts { get; set; } 
    }
}
