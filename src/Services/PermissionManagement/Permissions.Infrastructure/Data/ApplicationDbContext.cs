/*
 * This class represents the application's DbContext, which provides access to the database tables
 * and manages the database connection and schema.
 */

using Microsoft.EntityFrameworkCore;
using Permissions.Application.Data;
using Permissions.Domain.Models;
using System.Reflection;


namespace Permissions.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        // Constructor to initialize the DbContext with options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
           base(options)
        {

        }

        // DbSet for accessing the Employee table
        public DbSet<Employee> Employees => Set<Employee>();

        // DbSet for accessing the Permission table
        public DbSet<Permission> Permissions => Set<Permission>();

        // Method to configure the model using configurations from the current assembly
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Apply configurations for entities from the current assembly
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Call the base class's OnModelCreating method
            base.OnModelCreating(builder);
        }
    }
}
