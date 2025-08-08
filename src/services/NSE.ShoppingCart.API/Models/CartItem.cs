namespace NSE.ShoppingCart.API.Models;

public class CartItem
{
    public CartItem()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public Guid CustomerCartId { get; set; }
    public CustomerCart CustomerCart { get; set; }
}