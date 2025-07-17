namespace BoldChainBackendAPI.BoldChainException
{
    public abstract class ApiException : Exception
    {
        public abstract int StatusCode { get; }
        public string Type { get; }
        public string Title { get; }
        protected ApiException(string type, string title, string message) : base(message)
        {
            Type = type;
            Title = title;
        }


    }
    public class ValidationException : ApiException
    {
        public override int StatusCode => StatusCodes.Status400BadRequest;
        public IDictionary<string, string[]> Errors { get; }
        public ValidationException(IDictionary<string, string[]> errors)
            : base("http://localhost:4647/errors/validation", "Validation error", "one or more validation errors occured")
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }
    }
    public class NotFoundException : ApiException
    {
        public override int StatusCode => StatusCodes.Status404NotFound;
        public NotFoundException(string resourceName, object key)
            : base("http://localhost:4647/errors/not-found", "Resource not found", $"Resource '{resourceName}' with identifier '{key}' was not found")
        {
        }
    }
    public class UnauthorizedException : ApiException
    {
        public override int StatusCode => StatusCodes.Status401Unauthorized;
        public UnauthorizedException(string message)
            : base("http://localhost:4647/errors/unauthorized", "Unauthorized", message)
        {
        }
    }
    public class ForbiddenException : ApiException
    {
        public override int StatusCode => StatusCodes.Status403Forbidden;
        public ForbiddenException(string message)
            : base("http://localhost:4647/errors/forbidden", "Forbidden", message)
        {
        }
    }
    public class InternalServerErrorException : ApiException
    {
        public override int StatusCode => StatusCodes.Status500InternalServerError;
        public InternalServerErrorException(string message)
            : base("http://localhost:4647/errors/internal-server-error", "Internal Server Error", message)
        {
        }

    }
}

