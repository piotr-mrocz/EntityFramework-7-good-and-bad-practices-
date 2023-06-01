using EntityFrameworkNews.Models.Dtos;

namespace Domain.Models.Dtos;
public record OrdersListDto(List<OrderDto> Orders, int TotalCount);