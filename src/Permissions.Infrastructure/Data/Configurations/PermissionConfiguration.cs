
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
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasConversion(
                permissionId => permissionId.Value,
                dbId => PermissionId.Of(dbId));

            builder.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(p => p.EmployeeId)
                .IsRequired();

            builder.Property(p => p.ApplicationName).IsRequired();
            builder.Property(p => p.ApplicationName).HasMaxLength(100);

            builder.Property(p => p.PermissionType)
                .HasDefaultValue(PermissionType.User)
                .HasConversion(
                pt => pt.ToString(),
                dbStatus => (PermissionType)Enum.Parse(typeof(PermissionType), dbStatus));

        }
    }
}
