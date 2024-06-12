
using Northwind.Shared.AspNetCore.Attributes;
using Northwind.Shared.AspNetCore.HttpFeatures;
using Northwind.Shared.AspNetCore.Interfaces;
using Northwind.Shared.AspNetCore.Models;
using System;
using System.Net;
using System.Reflection;

namespace Northwind.Shared.AspNetCore.Middlewares;

public class CustomHttpLoggingMiddleware : IMiddleware
{
    private readonly IHttpLoggingRepository _httpLoggingRepository;

    public CustomHttpLoggingMiddleware(IHttpLoggingRepository httpLoggingRepository)
    {
        _httpLoggingRepository = httpLoggingRepository;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var disableHttpLoggingAttribute = context.GetEndpoint()?.Metadata.GetMetadata<DisableHttpLogging>();
        if (disableHttpLoggingAttribute != null)
        {
            await next(context);
            return;
        }

        Stream originalBody = context.Response.Body;     
        try
        {
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await next(context);

            memoryStream.Position = 0;
            string? responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
            responseBody = string.IsNullOrWhiteSpace(responseBody) ? null : responseBody;

            await InvokeInternalAsync(context, responseBody);

            memoryStream.Position = 0;
            await memoryStream.CopyToAsync(originalBody);
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }

    private async Task InvokeInternalAsync(HttpContext context, string? responseBody)
    {
        var httpRequestTimeFeature = context.Features.Get<IHttpRequestTimeFeature>();
        var httpRequestElapsedMilliseconds = (DateTime.Now - httpRequestTimeFeature!.RequestDate).TotalMilliseconds;

        HttpLog httpLog = new HttpLog();
        httpLog.Id = Guid.NewGuid();
        httpLog.Host = context.Request.Host.ToString();
        httpLog.ApplicationName = Assembly.GetEntryAssembly()?.GetName().Name;
        httpLog.UserId = context.User.Identity?.Name;
        httpLog.ClientIPAddress = context.Connection.RemoteIpAddress?.ToString();
        httpLog.ClientName = await GetClientNameAsync(context);
        httpLog.CorrelationId = context.TraceIdentifier;
        httpLog.RequestPath = context.Request.Path;
        httpLog.RequestQueryString = context.Request.GetQueryString();
        httpLog.RequestMethod = context.Request.Method;
        httpLog.RequestHeader = context.Request.GetHeaders();
        httpLog.RequestBody = await context.Request.GetBodyAsync();
        httpLog.ResponseHeader = context.Response.GetHeaders();
        httpLog.ResponseBody = responseBody;
        httpLog.StatusCode = context.Response.StatusCode;
        httpLog.Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        httpLog.ExecutionTime = httpRequestTimeFeature.RequestDate;
        httpLog.ExecutionDuration = httpRequestElapsedMilliseconds;
        httpLog.Version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();

        await _httpLoggingRepository.InsertAsync(httpLog);
    }

    private async Task<string?> GetClientNameAsync(HttpContext context)
    {
        var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString();
        if (remoteIpAddress == null)
        {
            return null;
        }
        return (await Dns.GetHostEntryAsync(remoteIpAddress)).HostName;
    }
}
