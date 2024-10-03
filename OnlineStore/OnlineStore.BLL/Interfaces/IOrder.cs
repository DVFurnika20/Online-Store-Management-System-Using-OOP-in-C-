using OnlineStore.BLL.Services;

namespace OnlineStore.BLL.Interfaces
{
    public interface IOrder
    {
        bool CreateOrder(ICustomer customer, ProductService product, int quantity);
        bool AddProduct(ProductService product, int quantity);
        void SetCustomer(ICustomer customer);
        void ApplyDiscount(IDiscount discount);
        void SetPaymentMethod(IPayment paymentMethod);
        void CompleteOrder();
    }
}