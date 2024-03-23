﻿namespace GeekShopping.CouponApi.Dtos;

public class CouponDto
{
    public long Id { get; set; }
    public string CouponCode { get; set; } = null!;
    public decimal DiscountAmount { get; set; }
}
