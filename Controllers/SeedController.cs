using baiNho.Data;
using baiNho.Models;
using Microsoft.AspNetCore.Mvc;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace baiNho.Controllers;

public class SeedController : Controller
{
    private MyDataContext _context;

    public SeedController(MyDataContext context)
    {
        _context = context;
    }

    [Obsolete("Obsolete")]
    public IActionResult GenerateSeed()
    {
        var faker = new Faker();
        var fakerVi = new Faker("vi");
        var users = Enumerable.Range(1, 3).Select(i => new User()
        {
            Name = faker.Name.FullName(),
            Email = faker.Internet.Email(),
        }).ToList();
        foreach (var user in users)
        {
            Console.WriteLine($"Name: {user.Name}, Email: {user.Email}");
        }
        _context.Users.AddRange(users);
        var products = Enumerable.Range(1, 30).Select(i => new Product()
        {
            Name = faker.Commerce.ProductName(),
            Price = faker.Random.Int(10, 1000)
        }).ToList();
        foreach (var p in products)
        {
            Console.WriteLine($"Name: {p.Name}, Email: {p.Price}");
        }
        _context.Products.AddRange(products);
        var orders = Enumerable.Range(1, 10).Select(i => new Order()
        {
            User = users[faker.Random.Int(0, users.Count - 1)], 
            UserId = users[faker.Random.Int(0, users.Count - 1)].Id, 
            OrderDetails = Enumerable.Range(1, faker.Random.Int(2, 4)) 
                .Select(od => new OrderDetail()
                {
                    ProductId = faker.Random.Int(1, products.Count), 
                    Product = products[faker.Random.Int(0, products.Count - 1)], 
                    Quantity = faker.Random.Int(1, 5),
                    UnitPrice = decimal.Parse(faker.Commerce.Price()) 
                }).ToList()
        }).ToList();
        _context.Orders.AddRange(orders);
        _context.SaveChanges();
        return Ok("Ok");
    }
    public async Task<JsonResult> GetOrderDetailsAsync(int id)
    {
        var o = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        return Json(o);
    }
}