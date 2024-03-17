using Microsoft.EntityFrameworkCore;
using Permissions.Application.Data;
using Permissions.Domain.Models;
using System.Reflection;


namespace Permissions.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
           base(options)
        {

        }

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Permission> Permissions => Set<Permission>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
