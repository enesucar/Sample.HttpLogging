namespace Microsoft.AspNetCore.Http;

public static class HttpResponseExtensions
{
    public static string? GetHeaders(this HttpResponse httpResponse)
    {
        if (httpResponse.Headers.Keys.Count == 0)
        {
            return null;
        }

        string? header = httpResponse.Headers.Select(x => x.ToString()).Aggregate((key, value) => key + ", " + value);
        return string.IsNullOrEmpty(header) ? null : header;
    }
}
