using BenchmarkDotNet.Attributes;

namespace FirstVsSingle;

[MemoryDiagnoser]
public class OrderService
{
    private const int _entityPosition = 20000;
    private const int _productsCount = 10;
    private const int _userCount = 20;

    private const int _numberOfElements = 3000000;

    private List<Order> _orderList = new();

    public void RunSingleVsFirst()
    {
        _orderList = GetListOfOrders();

        SingleOrDefault();
        FirstOrDefault();
    }

    private List<Order> GetListOfOrders()
    {
        List<Order> orderList = new();

        var random = new Random();

        for (int number = 1; number <= _numberOfElements; number++)
        {
            var randomIdProduct = random.Next(_productsCount);
            var randomIdUser = random.Next(_userCount);

            orderList.Add(new()
            {
                IdProduct = randomIdProduct,
                IdUser = randomIdUser
            });
        }

        return orderList;
    }

    [Benchmark]
    public void SingleOrDefault()
    {
        var order = _orderList
            .SingleOrDefault(x => x.Id == _entityPosition);
    }

    [Benchmark]
    public void FirstOrDefault()
    {
        var order = _orderList
            .FirstOrDefault(x => x.Id == _entityPosition);
    }
}
