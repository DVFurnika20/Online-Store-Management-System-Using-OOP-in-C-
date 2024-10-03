using System;
using OnlineStore.BLL.Interfaces;
using System.Collections.Generic;

namespace OnlineStore.BLL.Services
{
    public class OrderService : IOrder
    {
        private ICustomer _customer;
        private ProductService _product;
        private Dictionary<ProductService, int> _products;
        private int _quantity;
        private decimal _totalPrice;
        private IDiscount _discount;
        private IPayment _paymentMethod;
        private bool _isOrderCompleted;
        
        public OrderService()
        {
            _products = new Dictionary<ProductService, int>();
        }

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
        
        public void SetCustomer(ICustomer customer)
        {
            _customer = customer;
        }
        
        public bool AddProduct(ProductService product, int quantity)
        {
            if (product.DeductStock(quantity))
            {
                if (_products.ContainsKey(product))
                {
                    _products[product] += quantity;
                }
                else
                {
                    _products[product] = quantity;
                }
                _totalPrice += product.Price * quantity;
                Console.WriteLine($"Added {quantity} unit(s) of {product.Name} to the order.");
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
            if (_customer == null)
            {
                Console.WriteLine("Customer not set. Cannot complete order.");
                return;
            }

            if (_paymentMethod == null)
            {
                Console.WriteLine("Payment method not set. Cannot complete order.");
                return;
            }

            if (_paymentMethod.ProcessPayment(_totalPrice))
            {
                _isOrderCompleted = true;
                foreach (var product in _products)
                {
                    Console.WriteLine($"{product.Value} units of {product.Key.Name} deducted from stock.");
                }
                Console.WriteLine($"Order completed for {_customer.FirstName} {_customer.LastName}. Products processed.");
            }
            else
            {
                Console.WriteLine("Payment failed. Order not completed.");
                foreach (var product in _products)
                {
                    product.Key.DeductStock(-product.Value);
                }
            }
        }
    }
}