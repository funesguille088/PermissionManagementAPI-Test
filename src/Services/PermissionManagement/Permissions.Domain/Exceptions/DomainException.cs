/*
   DomainException represents an exception specific to the domain layer.

   Usage:
   1. Throw DomainException to indicate domain-specific errors.
*/
namespace Permissions.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base($"Domain Exception: \"{message}\" throws from Domain Layer.")
        {
        }
    }
}
