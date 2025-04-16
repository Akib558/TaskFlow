using System;
using System.Net;

namespace TaskFlow.Exceptions
{
    public class BadRequestException : Exception
    {
        public int StatusCode { get; }

        public BadRequestException(string message, int statusCode = (int)HttpStatusCode.BadRequest)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}