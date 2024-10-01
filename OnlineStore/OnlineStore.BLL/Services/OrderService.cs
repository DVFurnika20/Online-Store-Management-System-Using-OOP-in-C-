using System;
using OnlineStore.BLL.Interfaces;

namespace OnlineStore.BLL.Services
{
    public class OrderService : IOrder
    {
        private ICustomer _customer;
        private ProductService _product;
        private int _quantity;
        private decimal _totalPrice;
        private IDiscount _discount;

        public bool CreateOrder(ICustomer customer, ProductService product, int quantity)
        {
            if (product.DeductStock(quantity))
            {
                _customer = customer;
                _product = product;
                _quantity = quantity;
                _totalPrice = product.Price * quantity;
                Console.WriteLine($"Order created for {quantity} unit(s) of {product.Name}.");
                return true;
            }
            Console.WriteLine($"Insufficient stock for {product.Name}. Available: {product.GetAvailableStock()}");
            return false;
        }

        public void ApplyDiscount(IDiscount discount)
        {
            _discount = discount;
            _totalPrice = discount.ApplyDiscount(_totalPrice);
            Console.WriteLine("Discount applied. Final price: $" + _totalPrice.ToString("F2"));
        }

        public void CompleteOrder()
        {
            Console.WriteLine($"{_quantity} units of {_product.Name} deducted from stock.");
            Console.WriteLine($"Order completed for {_customer.FirstName} {_customer.LastName}. Product processed.");
        }
    }
}