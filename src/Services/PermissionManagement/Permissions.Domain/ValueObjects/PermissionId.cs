/*
   The PermissionId class represents the identifier of a permission in the domain model.

   Usage:
   1. Use the static Of method to create a new instance of the PermissionId class with a specified GUID value.
*/

using Permissions.Domain.Exceptions;

namespace Permissions.Domain.ValueObjects
{
    public record PermissionId
    {
        public Guid Value { get; }

        private PermissionId(Guid value) => Value = value;

        public static PermissionId Of(Guid value) 
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("PermissionId cannot be empty");
            }
            return new PermissionId(value); 
        }

    }
}
