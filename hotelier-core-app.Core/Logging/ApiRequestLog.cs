namespace hotelier_core_app.Core.Logging
{
    internal class ApiRequestLog
    {
        public string RequestUrl { get; set; }
        public string HttpMethod { get; set; }
        public string Request { get; set; }
        public string HttpResponseStatusCode { get; set; }
        public string Response { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
