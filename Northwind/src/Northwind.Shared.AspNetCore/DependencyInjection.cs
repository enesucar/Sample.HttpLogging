using Northwind.Shared.AspNetCore.Interfaces;
using Northwind.Shared.AspNetCore.Middlewares;
using Northwind.Shared.AspNetCore.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomHttpLogging(this IServiceCollection services)
    {
        return services.AddScoped<CustomHttpLoggingMiddleware>();
    }

    public static IServiceCollection AddEnableRequestBuffering(this IServiceCollection services)
    {
        return services.AddSingleton<EnableRequestBufferingMiddleware>();
    }

    public static IServiceCollection AddRequestTime(this IServiceCollection services)
    {
        return services.AddSingleton<RequestTimeMiddleware>();
    }

    public static IServiceCollection AddInMemoryHttpLoggingRepository(this IServiceCollection services)
    {
        return services.AddSingleton<IHttpLoggingRepository, InMemoryHttpLoggingRepository>();
    }
}

