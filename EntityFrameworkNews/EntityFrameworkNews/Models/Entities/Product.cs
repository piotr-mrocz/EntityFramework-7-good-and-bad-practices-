namespace EntityFrameworkNews.Models.Entities;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
