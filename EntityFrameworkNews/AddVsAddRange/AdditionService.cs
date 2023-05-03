using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Entities;

namespace AddVsAddRange;

public class AdditionService
{
    private readonly IApplicationDbContext _dbContext;

    public AdditionService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddUsersWithSaveChangesInLoop()
    {
        List<User> usersList = new()
        {
            new()
            {
                Login = "b.chrobry",
                FirstName = "Bolesław",
                LastName = "Chrobry"
            },
            new()
            {
                Login = "k.wielki",
                FirstName = "Kazimierz",
                LastName = "Wielki"
            },
            new()
            {
                Login = "a.macedonski",
                FirstName = "Aleksander",
                LastName = "Macedoński"
            },
            new()
            {
                Login = "i.grozny",
                FirstName = "Ivan",
                LastName = "IV Groźny"
            }
        };

        var start = DateTime.Now;

        foreach (var user in usersList)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        var stop = DateTime.Now;
        var time = (stop - start).TotalMilliseconds;

        Console.WriteLine($"Time: {time}");
    }

    public void AddUsersWithOnlyOneSaveChanges()
    {
        List<User> usersList = new()
        {
            new()
            {
                Login = "b.chrobry",
                FirstName = "Bolesław",
                LastName = "Chrobry"
            },
            new()
            {
                Login = "k.wielki",
                FirstName = "Kazimierz",
                LastName = "Wielki"
            },
            new()
            {
                Login = "a.macedonski",
                FirstName = "Aleksander",
                LastName = "Macedoński"
            },
            new()
            {
                Login = "i.grozny",
                FirstName = "Ivan",
                LastName = "IV Groźny"
            }
        };

        var start = DateTime.Now;

        foreach (var user in usersList)
            _dbContext.Users.Add(user);

        _dbContext.SaveChanges();

        var stop = DateTime.Now;
        var time = (stop - start).TotalMilliseconds;

        Console.WriteLine($"Time: {time}");
    }

    public void AddRangeUsers()
    {
        List<User> usersList = new()
        {
            new()
            {
                Login = "b.chrobry",
                FirstName = "Bolesław",
                LastName = "Chrobry"
            },
            new()
            {
                Login = "k.wielki",
                FirstName = "Kazimierz",
                LastName = "Wielki"
            },
            new()
            {
                Login = "a.macedonski",
                FirstName = "Aleksander",
                LastName = "Macedoński"
            },
            new()
            {
                Login = "i.grozny",
                FirstName = "Ivan",
                LastName = "IV Groźny"
            }
        };

        var start = DateTime.Now;

        _dbContext.Users.AddRange(usersList);
        _dbContext.SaveChanges();

        var stop = DateTime.Now;
        var time = (stop - start).TotalMilliseconds;

        Console.WriteLine($"Time: {time}");
    }
}
