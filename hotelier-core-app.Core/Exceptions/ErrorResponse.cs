namespace hotelier_core_app.Core.Exceptions
{
    public class ErrorResponse
    {
        public bool Status => false;
        public string Message { get; set; }
        public object? Data { get; set; }
    }
}
