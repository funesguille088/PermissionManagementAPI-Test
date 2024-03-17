using Microsoft.EntityFrameworkCore;
using Permissions.Domain.Models;

namespace Permissions.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Employee> Employees { get; }
    DbSet<Permission> Permissions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
