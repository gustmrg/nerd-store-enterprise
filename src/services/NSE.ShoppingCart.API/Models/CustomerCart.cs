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

    internal bool CartItemExists(CartItem item)
    {
        return Items.Any(x => x.ProductId == item.ProductId);
    }
    
    internal CartItem GetCartItemByProductId(Guid productId)
    {
        return Items.FirstOrDefault(x => x.ProductId == productId) ?? new CartItem();
    }
    
    internal void CalculateTotalAmount()
    {
        TotalAmount = Items.Sum(x => x.CalculateAmount());
    }
    
    internal void AddItem(CartItem item)
    {
        if (!item.IsValid()) return;
        
        item.SetCustomerCart(Id);

        if (CartItemExists(item))
        {
            var existingItem = GetCartItemByProductId(item.ProductId);
            existingItem.AddQuantity(item.Quantity);

            item = existingItem;
            Items.Remove(existingItem);
        }
        
        Items.Add(item);
        CalculateTotalAmount();
    }
}