using System;
using OnlineStore.BLL.Interfaces;
using OnlineStore.BLL.Services;
using OnlineStore.DAL.Models;

namespace OnlineStore.PL
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a customer
            var customerData = new Customer("John", "Doe");
            ICustomer customer = new CustomerService(customerData);

            // Create products
            var laptopData = new Product("Laptop", 1000.00m, 5);
            var smartphoneData = new Product("Smartphone", 700.00m, 2);
            var ebookData = new Product("E-Book", 50.00m, 100);

            var laptop = new ProductService(laptopData);
            var smartphone = new ProductService(smartphoneData);
            var ebook = new ProductService(ebookData);

            // Subscribe to the out-of-stock event
            laptop.OutOfStockEvent += message => Console.WriteLine($"[EVENT] {message}");
            smartphone.OutOfStockEvent += message => Console.WriteLine($"[EVENT] {message}");
            ebook.OutOfStockEvent += message => Console.WriteLine($"[EVENT] {message}");

            // Create orders
            var laptopOrder = new OrderService();
            var smartphoneOrder = new OrderService();
            var ebookOrder = new OrderService();

            // Place orders with validation and discounts
            if (laptopOrder.CreateOrder(customer, laptop, 3))
            {
                // Apply a 10% discount to the laptop order
                laptopOrder.ApplyDiscount(new PercentageDiscount(10));
                laptopOrder.CompleteOrder();
            }

            Console.WriteLine();

            if (ebookOrder.CreateOrder(customer, ebook, 1))
            {
                // Apply a $5 fixed discount on ebooks
                ebookOrder.ApplyDiscount(new FixedDiscount(5));
                ebookOrder.CompleteOrder();
            }

            Console.WriteLine();

            if (smartphoneOrder.CreateOrder(customer, smartphone, 2))
            {
                // Apply a $50 fixed discount on smartphones
                smartphoneOrder.ApplyDiscount(new FixedDiscount(50));
                smartphoneOrder.CompleteOrder();
            }

            Console.WriteLine();

            // Attempt to create an order with insufficient stock
            if (!laptopOrder.CreateOrder(customer, laptop, 3))
            {
                Console.WriteLine("Failed to create second laptop order due to insufficient stock.");
            }
        }
    }
}