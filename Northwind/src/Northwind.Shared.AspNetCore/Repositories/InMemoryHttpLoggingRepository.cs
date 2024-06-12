using Northwind.Shared.AspNetCore.Interfaces;
using Northwind.Shared.AspNetCore.Models;

namespace Northwind.Shared.AspNetCore.Repositories;

public class InMemoryHttpLoggingRepository : IHttpLoggingRepository
{
    private readonly List<HttpLog> httpLogs;

    public InMemoryHttpLoggingRepository()
    {
        httpLogs = [];
    }

    public async Task<IEnumerable<HttpLog>> GetPaginatedListAsync(
        int page = 1, 
        int size = 10)
    {
        var data = httpLogs.Skip((page - 1) * size).Take(size).ToList();
        return await Task.FromResult(data);
    }

    public async Task<HttpLog> InsertAsync(
        HttpLog httpLog, 
        CancellationToken cancellationToken = default)
    {
        httpLogs.Add(httpLog);
        return await Task.FromResult(httpLog);
    }
}
