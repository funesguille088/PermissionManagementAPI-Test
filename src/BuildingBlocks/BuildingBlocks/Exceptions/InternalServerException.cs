/*
     * InternalServerException: Represents an exception for internal server errors.
     * 
     * This exception is thrown when an unexpected error occurs on the server side.
     * It inherits from the base Exception class and provides additional details about the error.
     */
namespace BuildingBlocks.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string message) : base(message)
        {
        }

        public InternalServerException(string message, string details) : base(message)
        {
            Details = details;
        }

        public string? Details { get; }
    }
}
