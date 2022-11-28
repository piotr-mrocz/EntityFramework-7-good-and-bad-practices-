namespace EntityFrameworkNews.Models.Entities;

public class Order
{
    public int Id { get; set; }
    public int IdProduct { get; set; }
    public int IdUser { get; set; }

    public Product Product { get; set; } = new();
    public User User { get; set; } = new();
}
