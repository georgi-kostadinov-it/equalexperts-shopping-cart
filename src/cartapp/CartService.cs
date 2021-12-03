using System;
using System.Collections.Generic;
using System.Linq;
using CartAppUtils = cartapp.Utils;

namespace cartapp
{
  public class CartService : ICartService
  {
    private readonly List<ICartItem> _items = new List<ICartItem>();
    private decimal _salesTaxPercent = 0.0m;

    public decimal SalesTax 
    { 
      get { return this._salesTaxPercent; }

      set { 
        if (value < 0) throw new Exception("The Sales Tax Percent cannot be negative!");
        this._salesTaxPercent = value; 
        }
    }

    public decimal TotalPrice()
    {
      return CartAppUtils.RoundToTheNearestHundredth(UnRoundedTotal()) + TotalSalesTaxAmount();
    }

    public decimal TotalSalesTaxAmount()
    {
      var taxAmount = (UnRoundedTotal() * this._salesTaxPercent) / 100; 
      return CartAppUtils.RoundToTheNearestHundredth(taxAmount);
    }

    private decimal UnRoundedTotal()
    {
      return _items.Sum(_it => _it.Price * _it.Quantity);
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