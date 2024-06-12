namespace Northwind.Shared.AspNetCore.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DisableHttpLogging : Attribute
{
}
