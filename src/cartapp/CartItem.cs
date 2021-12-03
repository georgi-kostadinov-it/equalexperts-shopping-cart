using System;
using System.Collections.Generic;
using System.Linq;

namespace cartapp
{
  public class CartItem : ICartItem
  {
    public CartItem(string productName, int quantity, double price) 
    {
        if (String.IsNullOrEmpty(productName)) throw new Exception("Cart Item Product Name cannot be empty!");
        if (quantity <= 0) throw new Exception("Cart Item Quantity must be a positive number greater than zero!");
        if (price < 0) throw new Exception("Cart Item Price cannot be negative!");

        this.ProductName = productName;
        this.Quantity = quantity;
        this.Price = price;
    }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
  }
}