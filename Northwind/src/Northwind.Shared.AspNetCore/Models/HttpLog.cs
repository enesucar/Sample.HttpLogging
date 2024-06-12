namespace Northwind.Shared.AspNetCore.Models;

public class HttpLog
{
    public Guid Id { get; set; }

    public string Host { get; set; }

    public string? ApplicationName { get; set; }

    public string? UserId { get; set; }

    public string CorrelationId { get; set; }

    public string? ClientIPAddress { get; set; }

    public string? ClientName { get; set; }

    public string RequestPath { get; set; }

    public string? RequestQueryString { get; set; }

    public string RequestMethod { get; set; }

    public string? RequestHeader { get; set; }

    public string? RequestBody { get; set; }

    public string? ResponseHeader { get; set; }

    public string? ResponseBody { get; set; }

    public int StatusCode { get; set; }

    public DateTime ExecutionTime { get; set; }

    public double ExecutionDuration { get; set; }

    public string? Environment { get; set; }

    public string? Version { get; set; }

    public HttpLog()
    {
        Host = null!;
        ApplicationName = null!;
        ClientIPAddress = null!;
        ClientName = null!;
        CorrelationId = null!;
        RequestPath = null!;
        RequestMethod = null!;
        Version = null!;
    }
}
