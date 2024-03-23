/*
   The EmployeeId class represents the identifier of an employee in the domain model.

   Usage:
   1. Use the static Of method to create a new instance of the EmployeeId class with a specified GUID value.
*/

using Permissions.Domain.Exceptions;

namespace Permissions.Domain.ValueObjects
{
    public record EmployeeId
    {
        public Guid Value { get; }

        private EmployeeId(Guid value) => Value = value;

        public static EmployeeId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("EmployeeId cannot be empty");
            }

            return new EmployeeId(value);
        }

    }
}
