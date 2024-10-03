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
        private IPayment _paymentMethod;
        private bool _isOrderCompleted;

        public bool CreateOrder(ICustomer customer, ProductService product, int quantity)
        {
            if (product.DeductStock(quantity))
            {
                _customer = customer;
                _product = product;
                _quantity = quantity;
                _totalPrice = product.Price * quantity;
                _isOrderCompleted = false;
                Console.WriteLine($"Order created for {quantity} unit(s) of {product.Name}.");
                return true;
            }
            Console.WriteLine($"Insufficient stock for {product.Name}. Available: {product.GetAvailableStock()}");
            return false;
        }

        public void ApplyDiscount(IDiscount discount)
        {
            _discount = discount;
            _totalPrice = discount.ApplyDiscount(_totalPrice, _quantity);
            Console.WriteLine("Discount applied. Final price: $" + _totalPrice.ToString("F2"));
        }

        public void SetPaymentMethod(IPayment paymentMethod)
        {
            _paymentMethod = paymentMethod;
        }

        public void CompleteOrder()
        {
            if (_paymentMethod == null)
            {
                Console.WriteLine("Payment method not set. Cannot complete order.");
                return;
            }

            if (_paymentMethod.ProcessPayment(_totalPrice))
            {
                _isOrderCompleted = true;
                Console.WriteLine($"{_quantity} units of {_product.Name} deducted from stock.");
                Console.WriteLine($"Order completed for {_customer.FirstName} {_customer.LastName}. Product processed.");
            }
            else
            {
                Console.WriteLine("Payment failed. Order not completed.");
                
                _product.DeductStock(-_quantity);
            }
        }
    }
}