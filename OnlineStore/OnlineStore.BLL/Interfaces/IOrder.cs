using OnlineStore.BLL.Services;

namespace OnlineStore.BLL.Interfaces
{
    public interface IOrder
    {
        bool CreateOrder(ICustomer customer, ProductService product, int quantity);
        void ApplyDiscount(IDiscount discount);
        void CompleteOrder();
    }
}