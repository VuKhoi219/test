namespace baiNho.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set;}
    public int Price { get; set;}
    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}