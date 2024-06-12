using Northwind.API.Interfaces;
using Northwind.API.Models;

namespace Northwind.API.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products;

    public InMemoryProductRepository()
    {
        _products = [];
        SeedData();
    }

    public List<Product> GetList()
    {
        return _products.Where(o => !o.IsDeleted).ToList();
    }

    public Product? GetById(int id)
    {
        return _products.FirstOrDefault(o => o.Id == id && !o.IsDeleted);
    }

    public Product Create(Product product)
    {
        _products.Add(product);
        return product;
    }

    public void Update(Product product)
    {
        var index = _products.FindIndex(o => o.Id == product.Id && !o.IsDeleted);
        if (index != -1)
        {
            _products[index] = product;
        }
    }

    public void Delete(int id)
    {
        var product = _products.FirstOrDefault(o => o.Id == id && !o.IsDeleted);
        if (product != null)
        {
            _products.Remove(product);
        }
    }

    private void SeedData()
    {
        _products.AddRange(new List<Product>()
        {
            new Product() { Id = 1, Name = "Akıllı LED Lamba", Price = 29.99m, IsDeleted = false },
            new Product() { Id = 2, Name = "Kablosuz Şarj Cihazı", Price = 39.99m, IsDeleted = false },
            new Product() { Id = 3, Name = "Sporcu Kulaklık", Price = 34.99m, IsDeleted = false },
            new Product() { Id = 4, Name = "Yüksek Performanslı Fare", Price = 24.99m, IsDeleted = false },
            new Product() { Id = 5, Name = "Bluetooth Kulaklık", Price = 35.99m, IsDeleted = false },
            new Product() { Id = 6, Name = "Temizlik Robotu", Price = 149.99m, IsDeleted = true }
        });
    }
}
