using FluentValidation;

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
    
    internal void SetCustomerCart(Guid customerCartId)
    {
        CustomerCartId = customerCartId;
    }
    
    internal decimal CalculateAmount()
    {
        return Price * Quantity;
    }
    
    internal void AddQuantity(int quantity)
    {
        if (quantity <= 0) return;
        
        Quantity += quantity;
    }
    
    internal void UpdateQuantity(int quantity)
    {
        if (quantity <= 0) return;
        
        Quantity = quantity;
    }

    internal bool IsValid()
    {
        return new CartItemValidator().Validate(this).IsValid;
    }
    
    public class CartItemValidator : AbstractValidator<CartItem>
    {
        public CartItemValidator()
        {
            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product ID is required.");
            
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(2, 200).WithMessage("Product name must be between 2 and 200 characters.");
            
            RuleFor(c => c.Price)
                .GreaterThan(0).WithMessage(item => $"The {item.Name} price must be greater than zero.");

            RuleFor(c => c.Quantity)
                .GreaterThan(0).WithMessage(item => $"{item.Name} quantity must be greater than zero.");

            RuleFor(c => c.Quantity)
                .LessThan(15).WithMessage(item => $"Maximum quantity for item {item.Name} allowed is 15.");
        }
    }
}