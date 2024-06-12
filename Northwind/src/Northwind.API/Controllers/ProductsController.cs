using Microsoft.AspNetCore.Mvc;
using Northwind.API.Interfaces;
using Northwind.API.Models;

namespace Northwind.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public IActionResult GetList()
    {
        var products = _productRepository.GetList();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var product = _productRepository.GetById(id);
        return Ok(product);
    }

    [HttpPost]
    public IActionResult Create(Product product) 
    {
        var createdProduct = _productRepository.Create(product);
        return Created("", createdProduct);
    }

    [HttpPut]
    public IActionResult Edit([FromBody] Product product)
    {
        _productRepository.Update(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        _productRepository.Delete(id);
        return NoContent();
    }
}
