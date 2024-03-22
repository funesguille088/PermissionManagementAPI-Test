/*
     * NotFoundException: Represents an exception for resource not found scenarios.
     * 
     * This exception is thrown when a requested resource is not found.
     * It inherits from the base Exception class and provides constructors to set error messages.
     */
namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
