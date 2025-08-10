using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSE.ShoppingCart.API.Data;
using NSE.ShoppingCart.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.User;

namespace NSE.ShoppingCart.API.Controllers;

[Authorize]
public class ShoppingCartController : MainController
{
    private readonly IApplicationUser _applicationUser;
    private readonly ShoppingCartContext _context;

    public ShoppingCartController(IApplicationUser applicationUser, ShoppingCartContext context)
    {
        _applicationUser = applicationUser;
        _context = context;
    }
    
    [HttpGet("cart")]
    public async Task<CustomerCart> GetCart()
    {
        return await GetCustomerCart() ?? new CustomerCart
        {
            CustomerId = _applicationUser.GetUserId(),
            Items = new List<CartItem>()
        };
    }
    
    [HttpPost("cart")]
    public async Task<IActionResult> AddItemToCart(CartItem item)
    {
        var cart = await GetCustomerCart();

        if (cart is null)
        {
            HandleNewCart(item);
        }
        else
        {
            HandleExistingCart(cart, item); 
        }

        if (HasErrors()) return CustomResponse();

        await SaveDataAsync();
        
        return CustomResponse();
    }
    
    [HttpPut("cart/{productId}")]
    public async Task<IActionResult> UpdateCartItem(Guid productId, CartItem item)
    {
        var cart = await GetCustomerCart();
        var cartItem = await GetValidCartItem(productId, cart, item);
        if (cartItem is null) return CustomResponse();
        
        cart.UpdateItemQuantity(cartItem, item.Quantity);
        
        _context.CartItems.Update(cartItem);
        _context.CustomerCarts.Update(cart);
        
        await SaveDataAsync();
        
        return CustomResponse();
    }
    
    [HttpDelete("cart/{productId}")]
    public async Task<IActionResult> RemoveItemFromCart(Guid productId)
    {
        var cart = await GetCustomerCart();
        var cartItem = await GetValidCartItem(productId, cart);
        if (cartItem is null) return CustomResponse();
        
        cart.RemoveItem(cartItem);
        
        _context.CartItems.Remove(cartItem);
        _context.CustomerCarts.Update(cart);
        
        await SaveDataAsync();
        
        return CustomResponse();
    }

    private async Task<CustomerCart?> GetCustomerCart()
    {
        return await _context.CustomerCarts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == _applicationUser.GetUserId());
    }
    
    private void HandleNewCart(CartItem item)
    {
        var newCart = new CustomerCart(_applicationUser.GetUserId());
        newCart.AddItem(item);
     
        _context.CustomerCarts.Add(newCart);
    }
    
    private void HandleExistingCart(CustomerCart cart, CartItem item)
    {
        var existingItem = cart.CartItemExists(item);
        
        cart.AddItem(item);
        
        if (existingItem)
        {
            _context.CartItems.Update(cart.GetCartItemByProductId(item.ProductId));
        }
        else
        {
            _context.CartItems.Add(item);
        }
        
        _context.CustomerCarts.Update(cart);
    }

    private async Task<CartItem?> GetValidCartItem(Guid productId, CustomerCart cart, CartItem? item = null)
    {
        if (item is not null && productId != item.ProductId)
        {
            AddError($"The product ID does not match the item\'s product ID ({item.ProductId}).");
        }

        if (cart == null)
        {
            AddError("Cart is not valid.");
            return null;
        }
        
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(i => i.CustomerCartId == cart.Id && i.ProductId == productId);
        
        if (cartItem is null || !cart.CartItemExists(item))
        {
            AddError("Item is not in customer cart.");
            return null;
        }
        
        return cartItem;
    }

    private async Task SaveDataAsync()
    {
        var result = await _context.SaveChangesAsync();
        if (result <= 0) AddError("Data could not be saved to the database.");
    }
}