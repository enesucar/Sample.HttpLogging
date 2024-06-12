using Microsoft.AspNetCore.Mvc;
using Northwind.Shared.AspNetCore.Attributes;
using Northwind.Shared.AspNetCore.Interfaces;

namespace Northwind.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HttpLogsController : ControllerBase
{
    private readonly IHttpLoggingRepository _httpLoggingRepository;

    public HttpLogsController(IHttpLoggingRepository httpLoggingRepository)
    {
        _httpLoggingRepository = httpLoggingRepository;
    }

    [HttpGet]
    [DisableHttpLogging]
    public async Task<IActionResult> GetList()
    {
        var httpLogs = await _httpLoggingRepository.GetPaginatedListAsync(1, 1000);
        return Ok(httpLogs);
    }
}
