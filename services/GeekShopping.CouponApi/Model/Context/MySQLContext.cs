using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponApi.Model.Context;

public class MySQLContext : DbContext
{
    public MySQLContext() { }
    public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
    { }

    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    CouponCode = "TESTE_2024_10",
                    DiscountAmount = 10
                },
                new Coupon
                {
                    Id = 2,
                    CouponCode = "TESTE_2024_15",
                    DiscountAmount = 15
                });
    }
}
