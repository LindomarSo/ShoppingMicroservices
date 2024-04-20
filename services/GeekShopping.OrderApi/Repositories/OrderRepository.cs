using GeekShopping.OrderApi.Models;
using GeekShopping.OrderApi.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderApi.Repositories;

public class OrderRepository(DbContextOptions<MySQLContext> context) : IOrderRepository
{
    public async Task<bool> AddOrderAsync(OrderHeader order)
    {
        await using var _db = new MySQLContext(context);

        _db.OrderHeaders.Add(order);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task UpdateOrderPaymentStatusAsync(long orderHeaderId, bool paid)
    {
        await using var _db = new MySQLContext(context);

        var header = await _db.OrderHeaders.FirstOrDefaultAsync(o => o.Id == orderHeaderId);

        if (header is not null)
        {
            header.PaymentStatus = paid;
            await _db.SaveChangesAsync();
        }
    }
}
