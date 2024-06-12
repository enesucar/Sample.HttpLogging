using Northwind.API.Models;

namespace Northwind.API.Interfaces;

public interface IProductRepository
{
    List<Product> GetList();

    Product? GetById(int id);

    Product Create(Product product);

    void Update(Product product);

    void Delete(int id);
}
