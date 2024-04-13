using Microsoft.Extensions.Logging;

namespace Gallery.Shared.Helpers
{
    public class SuperException : Exception
    {
        public SuperException(string message, ILogger<object> logger, Exception? exception) : base(message, exception)
        {
            logger.LogError(message, exception != null ? exception.Message : message, exception != null && exception.InnerException != null ? exception.InnerException : message);
        }
    }
}