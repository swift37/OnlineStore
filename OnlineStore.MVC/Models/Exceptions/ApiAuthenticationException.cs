namespace OnlineStore.MVC.Models.Exceptions
{
    public class ApiAuthenticationException : Exception
    {
        public string? SourceUrl { get; set; }

        public ApiAuthenticationException() : base() { }

        public ApiAuthenticationException(string? sourceUrl) : base() => 
            SourceUrl = sourceUrl;

        public ApiAuthenticationException(string? sourceUrl, string message) : base(message) =>
            SourceUrl = sourceUrl;

        public ApiAuthenticationException(string? sourceUrl, string message, Exception innerException) 
            : base(message, innerException) => SourceUrl = sourceUrl;
    }
}
