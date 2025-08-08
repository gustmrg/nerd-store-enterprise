namespace NSE.ShoppingCart.API.Models;

public class CustomerCart
{
    public CustomerCart()
    {
        
    }
    
    public CustomerCart(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
    }
    
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CartItem> Items { get; set; } = [];
}