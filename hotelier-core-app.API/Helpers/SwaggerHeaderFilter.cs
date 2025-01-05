using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace hotelier_core_app.API.Helpers
{
    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Security ??= new List<OpenApiSecurityRequirement>();

            OpenApiSecurityScheme openApiSecurityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearer"
                }
            };
            operation.Security.Add(new OpenApiSecurityRequirement { [openApiSecurityScheme] = new List<string>() });
        }
    }
}
