using System.Collections.Generic;

namespace cartapp 
{
  public interface ICartService 
  {
    double Total();
    List<ICartItem> Items();
    void Empty();
    void Add(ICartItem item);
  }
}