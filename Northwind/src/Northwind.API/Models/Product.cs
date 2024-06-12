namespace Northwind.API.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public bool IsDeleted { get; set; }

    public Product()
    {
        Name = null!;
    }
}
