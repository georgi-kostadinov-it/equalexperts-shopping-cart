using System.Collections.Generic;

namespace cartapp 
{
  public interface ICartService 
  {
    decimal TotalPrice();
    decimal TotalSalesTaxAmount();
    List<ICartItem> Items();
    void Empty();
    void Add(ICartItem item);
    decimal SalesTax { get; set; }
  }
}