namespace OnlineStore.BLL.Interfaces
{
    public interface IDiscount
    {
        decimal ApplyDiscount(decimal originalPrice);
    }
}