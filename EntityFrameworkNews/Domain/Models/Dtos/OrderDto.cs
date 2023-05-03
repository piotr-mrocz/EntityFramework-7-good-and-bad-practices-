namespace EntityFrameworkNews.Models.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public string Product { get; set; } = null!;
    public string User { get; set; } = null!;
}
