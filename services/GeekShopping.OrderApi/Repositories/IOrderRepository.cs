using GeekShopping.OrderApi.Models;

namespace GeekShopping.OrderApi.Repositories;

public interface IOrderRepository
{
    Task<bool> AddOrderAsync(OrderHeader order);
    Task UpdateOrderPaymentStatusAsync(long orderHeaderId, bool paid);
}
