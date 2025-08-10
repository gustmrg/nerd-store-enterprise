using FluentValidation;
using FluentValidation.Results;

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
    public ValidationResult ValidationResult { get; set; } = new();

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
    
    internal void UpdateItem(CartItem item)
    {
        item.SetCustomerCart(Id);

        var existingItem = GetCartItemByProductId(item.ProductId);
        
        Items.Remove(existingItem);
        Items.Add(item);

        CalculateTotalAmount();
    }
    
    internal void UpdateItemQuantity(CartItem item, int quantity)
    {
        if (quantity <= 0) return;
        
        item.UpdateQuantity(quantity);
        UpdateItem(item);
    }
    
    internal void RemoveItem(CartItem item)
    {
        var existingItem = GetCartItemByProductId(item.ProductId);
        Items.Remove(existingItem);
        
        CalculateTotalAmount();
    }
    
    internal bool IsValid()
    {
        var errors = Items.SelectMany(i => 
            new CartItem.CartItemValidator().Validate(i).Errors).ToList();
        
        errors.AddRange(new CustomerCartValidator().Validate(this).Errors);
        
        ValidationResult = errors.Any() ? new ValidationResult(errors) : new ValidationResult();
        
        return ValidationResult.IsValid;
    }
    
    public class CustomerCartValidator : AbstractValidator<CustomerCart>
    {
        public CustomerCartValidator()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer ID is invalid.");
            
            RuleFor(c => c.Items)
                .NotEmpty().WithMessage("The cart must contain at least one item.");
            
            RuleFor(c => c.TotalAmount)
                .GreaterThan(0).WithMessage("The cart total amount must be greater than zero.");
            
            RuleForEach(c => c.Items)
                .SetValidator(new CartItem.CartItemValidator());
        }
    }
}