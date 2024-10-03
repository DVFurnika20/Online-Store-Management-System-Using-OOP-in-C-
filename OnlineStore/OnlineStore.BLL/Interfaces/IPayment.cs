namespace OnlineStore.BLL.Interfaces
{
    public interface IPayment
    {
        bool ProcessPayment(decimal amount);
    }
}