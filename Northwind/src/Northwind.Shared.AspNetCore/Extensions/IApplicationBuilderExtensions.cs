
using Northwind.Shared.AspNetCore.Middlewares;

namespace Microsoft.AspNetCore.Builder;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomHttpLogging(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<CustomHttpLoggingMiddleware>();
    }

    public static IApplicationBuilder UseEnableRequestBuffering(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<EnableRequestBufferingMiddleware>();
    }

    public static IApplicationBuilder UseRequestTime(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<RequestTimeMiddleware>();
    }
}
