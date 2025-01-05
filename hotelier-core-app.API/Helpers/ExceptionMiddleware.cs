using hotelier_core_app.Model.DTOs.Response;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace hotelier_core_app.API.Helpers
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new BaseResponse
                        {
                            Status = false,
                            Message = contextFeature.Error.Message
                        }));
                    }
                });
            });
        }
    }
}
