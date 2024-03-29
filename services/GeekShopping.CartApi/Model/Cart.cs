﻿namespace GeekShopping.CartApi.Model;

public class Cart
{
    public CartHeader? CartHeader { get; set; }
    public IEnumerable<CartDetail> CartDetail { get; set; } = Enumerable.Empty<CartDetail>();
}
