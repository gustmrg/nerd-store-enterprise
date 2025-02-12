namespace NSE.WebApp.MVC.Models;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ImageUrl { get; set; }
    public int Quantity { get; set; }
}