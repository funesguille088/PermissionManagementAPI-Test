/*
   The Employee class represents an employee entity in the domain model.

   Usage:
   1. Define properties for storing employee-related data.
   2. Use the static Create method to create a new instance of the Employee class.
*/
using Permissions.Domain.Abstractions;
using Permissions.Domain.ValueObjects;

namespace Permissions.Domain.Models
{
    public class Employee : Entity<EmployeeId>
    {
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string SSO { get; private set; } = default!;

        public static Employee Create(EmployeeId id, string firstName, string lastName, string email, string sso)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(sso);

            var employee = new Employee()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                SSO = sso
            };
            return employee;
        }

    }
}
