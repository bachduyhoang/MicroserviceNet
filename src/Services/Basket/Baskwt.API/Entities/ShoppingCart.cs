﻿namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string? UserName { get; set; }
        public List<ShoppingCartItem>? Items { get; set; } = new List<ShoppingCartItem>();
        public ShoppingCart() 
        { 
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalProce
        {
            get
            {
                if(Items == null)
                {
                    return 0;
                }

                decimal total = 0;
                foreach (ShoppingCartItem item in Items)
                {
                    total += item.Price * item.Quantity;
                }
                return total;
            }
        }
    }

}
