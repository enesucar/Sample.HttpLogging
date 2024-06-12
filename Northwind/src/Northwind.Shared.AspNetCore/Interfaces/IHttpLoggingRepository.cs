using Northwind.Shared.AspNetCore.Models;

namespace Northwind.Shared.AspNetCore.Interfaces;

public interface IHttpLoggingRepository
{
    Task<IEnumerable<HttpLog>> GetPaginatedListAsync(
        int page = 1,
        int size = 10);

    Task<HttpLog> InsertAsync(HttpLog httpLog, CancellationToken cancellationToken = default); 
}
