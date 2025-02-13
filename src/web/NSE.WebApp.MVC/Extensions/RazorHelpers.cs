namespace NSE.WebApp.MVC.Extensions;

public class RazorHelpers
{
    public static string GetStockMessage(int quantity)
    {
        return quantity > 0 ? $"Apenas {quantity} em estoque!" : "Produto esgotado!";
    }

    public static string FormatPrice(decimal price)
    {
        return price > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", price) : "Gratuito";
    }
}