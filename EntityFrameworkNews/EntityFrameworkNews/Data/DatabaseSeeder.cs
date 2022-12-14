using EntityFrameworkNews.Helper;
using EntityFrameworkNews.Models.Entities;

namespace EntityFrameworkNews.Data;

public class DatabaseSeeder
{
    private readonly ApplicationDbContext _dbContext;

	public DatabaseSeeder(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void Seed()
	{
		if (!_dbContext.Database.CanConnect())
			return;

		if (!_dbContext.Users.Any())
			AddSomeUsers();

        if (!_dbContext.Products.Any())
            AddSomeProducts();

        if (!_dbContext.Orders.Any())
            AddSomeOrders();
    }

    private void AddSomeUsers()
	{
		var usersList = new List<User>()
		{
			new User()
			{
				FirstName = "Adam",
				LastName = "Małysz",
				Login = GetLogin("Adam", "Małysz")
            },
            new User()
            {
                FirstName = "Michał",
                LastName = "Anioł",
                Login = GetLogin("Michał", "Anioł")
            },
            new User()
            {
                FirstName = "Adam",
                LastName = "Mickiewicz",
                Login = GetLogin("Adam", "Mickiewicz")
            },
            new User()
            {
                FirstName = "Aleksander",
                LastName = "Wielki",
                Login = GetLogin("Aleksander", "Wielki")
            },
            new User()
            {
                FirstName = "Maria",
                LastName = "Konopnicka",
                Login = GetLogin("Maria", "Konopnicka")
            },
            new User()
            {
                FirstName = "Joanna",
                LastName = "d’Arc",
                Login = GetLogin("Joanna", "d’Arc")
            },
            new User()
            {
                FirstName = "Coco",
                LastName = "Channel",
                Login = GetLogin("Coco", "Channel")
            }
        };

		_dbContext.Users.AddRange(usersList);
		_dbContext.SaveChanges();
	}

	private string GetLogin(string firstName, string lastName)
	{
		var userLogin = $"{firstName.First()}.{lastName}";
		userLogin = StringHelper.ChangePolishLettersToEnglish(userLogin);
		return userLogin;
    }

    private void AddSomeProducts()
    {
        var productList = new List<Product>();
        var maxNumberOfProducts = 10;

        for (int number = 1; number <= maxNumberOfProducts; number++)
        {
            productList.Add(new Product()
            {
                ProductName = $"Product {number}"
            });
        }

        _dbContext.Products.AddRange(productList);
        _dbContext.SaveChanges();
    }

    private void AddSomeOrders()
    {
        var idsUsers = _dbContext.Users.Select(x => x.Id).ToList();
        var idsProducts = _dbContext.Products.Select(x => x.Id).ToList();

        var ordersNumber = 10000;
        var random = new Random();

        var orderList = new List<Order>();

        for (int number = 0; number < ordersNumber; number++)
        {
            var randomIdProduct = random.Next(idsProducts.Count);
            var randomIdUser = random.Next(idsUsers.Count);

            var idProduct = idsProducts.FirstOrDefault(x => x == randomIdProduct);
            var idUser = idsUsers.FirstOrDefault(x => x == randomIdUser);

            orderList.Add(new Order()
            {
                IdProduct = idProduct != default ? idProduct : idsProducts.FirstOrDefault(),
                IdUser = idUser != default ? idUser : idsUsers.FirstOrDefault()
            });
        }

        _dbContext.Orders.AddRange(orderList);
        _dbContext.SaveChanges();
    }
}
