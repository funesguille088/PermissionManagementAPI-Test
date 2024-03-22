
/*
     * BadRequestException: Represents an exception for bad request scenarios.
     * 
     * This exception is thrown when a request is invalid or malformed.
     * It inherits from the base Exception class and provides additional details about the error.
     */

namespace BuildingBlocks.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, string details) : base(message)
        {
            Details = details;
        }

        public string? Details { get; }
    }
}
