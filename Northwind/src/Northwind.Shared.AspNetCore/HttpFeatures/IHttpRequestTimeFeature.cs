namespace Northwind.Shared.AspNetCore.HttpFeatures;

public interface IHttpRequestTimeFeature
{
    DateTime RequestDate { get; }
}
