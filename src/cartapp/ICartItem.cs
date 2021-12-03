namespace cartapp 
{
  public interface ICartItem 
  {
    string ProductName { get; set; }
    int Quantity { get; set; }
    decimal Price { get; set; }
  }
}