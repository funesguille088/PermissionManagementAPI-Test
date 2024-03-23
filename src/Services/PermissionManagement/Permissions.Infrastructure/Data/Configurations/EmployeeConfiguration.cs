/*
 * This class configures the entity framework mapping for the Employee entity.
 */


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Permissions.Domain.Models;
using Permissions.Domain.ValueObjects;

namespace Permissions.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Set the primary key
            builder.HasKey(e => e.Id);

            // Configure the Id property to use value conversion
            builder.Property(e => e.Id).HasConversion(
            employeeId => employeeId.Value,
                dbId => EmployeeId.Of(dbId));

            // Configure the FirstName property
            builder.Property(e => e.FirstName).HasMaxLength(100).IsRequired();

            // Configure the LastName property
            builder.Property(e => e.LastName).HasMaxLength(100).IsRequired();

            // Configure the Email property with maximum length and uniqueness constraint
            builder.Property(e => e.Email).HasMaxLength(255);
            builder.HasIndex(e => e.Email).IsUnique();

            // Configure the SSO property with maximum length and uniqueness constraint
            builder.Property(e => e.SSO).HasMaxLength(100).IsRequired(); 
            builder.HasIndex(e => e.SSO).IsUnique();

        }
    }
}
