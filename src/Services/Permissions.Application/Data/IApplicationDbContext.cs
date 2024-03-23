/*
   The IApplicationDbContext interface defines the contract for the application database context.

   Usage:
   1. Define your own implementation of IApplicationDbContext.
   2. Use the interface to access the Employees and Permissions entities in the database.
*/

using Microsoft.EntityFrameworkCore;
using Permissions.Domain.Models;

namespace Permissions.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Employee> Employees { get; }
    DbSet<Permission> Permissions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
