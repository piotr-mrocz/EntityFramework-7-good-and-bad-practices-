namespace EntityFrameworkNews.Models.Entities;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
