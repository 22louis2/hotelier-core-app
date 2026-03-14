using hotelier_core_app.Migrations;

namespace hotelier_core_app.API.Middleware
{
    /// <summary>
    /// Middleware for resolving and setting the tenant schema based on the incoming request.
    /// </summary>
    public class TenantMiddleware
    {
        /// <summary>
        /// The next middleware in the pipeline.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        public TenantMiddleware(RequestDelegate next) => _next = next;

        /// <summary>
        /// Invokes the middleware to resolve the tenant and set the schema for the request.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="tenantProvider">The tenant provider to set the schema.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context, ITenantProvider tenantProvider)
        {
            // Example: resolve tenant from header, claims, or subdomain
            // Here, we use a header for demonstration
            var tenantId = context.Request.Headers["X-Tenant-Id"].ToString();
            var schema = string.IsNullOrWhiteSpace(tenantId) ? "public" : $"tenant_{tenantId}";
            tenantProvider.SetSchema(schema);
            await _next(context);
        }
    }
}
