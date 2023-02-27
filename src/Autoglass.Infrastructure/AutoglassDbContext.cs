using Autoglass.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Autoglass.Infrastructure
{
    public class AutoglassDbContext : DbContext
    {
        public AutoglassDbContext()
        {
        }

        public AutoglassDbContext(DbContextOptions<AutoglassDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
