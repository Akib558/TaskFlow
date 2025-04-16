using System.Net;

namespace TaskFlow.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public int StatusCode { get; }

        public NotFoundException(string message, int statusCode = (int)HttpStatusCode.NotFound)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}