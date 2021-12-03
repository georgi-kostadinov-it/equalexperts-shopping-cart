namespace cartapp 
{
  public interface ICartItem 
  {
    string ProductName { get; set; }
    int Quantity { get; set; }
    double Price { get; set; }
  }
}