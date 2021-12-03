using System;
using System.Collections.Generic;
using System.Linq;

namespace cartapp
{
  public class CartService : ICartService
  {
    private readonly List<ICartItem> _items = new List<ICartItem>();

    // All totals should be rounded up to 2 decimal places, 
    // i.e. 0.565 should result in 0.57 but 0.5649 should result in 0.56.
    public double Total()
    {
      return Math.Round(_items.Sum(_it => _it.Price * _it.Quantity), 2);
    }
    public List<ICartItem> Items()
    {
        return _items;
    }
    public void Empty()
    {
        _items.Clear();
    }
    public void Add(ICartItem item)
    {
      var results = _items.Where(o => o.ProductName.Equals(item.ProductName));
      var it = _items.FirstOrDefault(s => s.ProductName.Equals(item.ProductName));
      if (it != null)
      {
        it.Quantity = it.Quantity + item.Quantity;
      }
      else
      {
        _items.Add(item);
      }
    }
  }
}