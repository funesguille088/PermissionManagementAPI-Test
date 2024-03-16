using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Permissions.Domain.Abstractions;
using Permissions.Domain.Models;
using Permissions.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(
                employeeId => employeeId.Value,
                dbId => EmployeeId.Of(dbId));

            builder.Property(e => e.FirstName).HasMaxLength(100).IsRequired();

            builder.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            
            builder.Property(e => e.Email).HasMaxLength(255);
            builder.HasIndex(e => e.Email).IsUnique();
            
            builder.Property(e => e.SSO).HasMaxLength(100).IsRequired(); 
            builder.HasIndex(e => e.SSO).IsUnique();

        }
    }
}
