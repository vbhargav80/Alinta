using System.Reflection;
using Alinta.CodingTest.Models;
using Microsoft.EntityFrameworkCore;

namespace Alinta.CodingTest.Data
{
    public class AlintaDbContext : DbContext
    {
        public AlintaDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
