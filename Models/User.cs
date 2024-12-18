using System.ComponentModel.DataAnnotations;

namespace baiNho.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();
}