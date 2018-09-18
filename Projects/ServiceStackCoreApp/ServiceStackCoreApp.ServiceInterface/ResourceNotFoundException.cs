using System;
using System.Runtime.Serialization;

namespace ServiceStackCoreApp.ServiceInterface
{
    /// <summary>
    /// Because people like mapping exceptions to error codes.
    /// </summary>
    /// <remarks>
    /// Catching exceptions is not the performance intensive part, it's throwing them. Just think, the stack trace needs to be captured.
    /// Throwing exceptions for workflows where something is expected, like a search result not being found, can lead to programming by exception, which has maintainability issues.
    /// </remarks>
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string message) : base(message)
        {
        }

        public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}