/*
 * This class configures the entity framework mapping for the Permission entity.
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Permissions.Domain.Enums;
using Permissions.Domain.Models;
using Permissions.Domain.ValueObjects;

namespace Permissions.Infrastructure.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            // Set the primary key

            builder.HasKey(p => p.Id);

            // Configure the Id property to use value conversion
            builder.Property(p => p.Id).HasConversion(
                permissionId => permissionId.Value,
                dbId => PermissionId.Of(dbId));

            // Define the relationship with Employee entity
            builder.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(p => p.EmployeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientNoAction);

            // Configure the ApplicationName property
            builder.Property(p => p.ApplicationName).IsRequired();
            builder.Property(p => p.ApplicationName).HasMaxLength(100);

            // Configure the PermissionType property with default value and conversion
            builder.Property(p => p.PermissionType)
                .HasDefaultValue(PermissionType.User)
                .HasConversion(
                pt => pt.ToString(),
                dbStatus => (PermissionType)Enum.Parse(typeof(PermissionType), dbStatus));

            // Configure the PermissionGranted property
            builder.Property(p => p.PermissionGranted).IsRequired();

            // Define the relationship with the granted Employee entity
            builder.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(p => p.PermissionGrantedEmployeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientNoAction);

        }
    }
}
