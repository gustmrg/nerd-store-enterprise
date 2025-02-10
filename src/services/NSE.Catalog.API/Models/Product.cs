using NSE.Core.DomainObjects;

namespace NSE.Catalog.API.Models;

public class Product : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ImageUrl { get; set; }
    public int Quantity { get; set; }
}