using System.Net;
using System.Runtime.Serialization;

namespace hotelier_core_app.Core.Exceptions
{
    public abstract class BaseException : Exception
    {
        public abstract HttpStatusCode StatusCode { get; }

        public object? Details { get; set; }

        protected BaseException(string message, object? details = null)
            : base(message)
        {
            Details = details;
        }

        protected BaseException(string message, Exception inner, object? details = null)
            : base(message, inner)
        {
            Details = details;
        }

        protected BaseException(SerializationInfo info, StreamingContext context, object? details = null)
            : base(info, context)
        {
            Details = details;
        }

        public ErrorResponse CreateErrorResponse()
        {
            return new ErrorResponse
            {
                Message = Message,
                Data = Details
            };
        }
    }
}
