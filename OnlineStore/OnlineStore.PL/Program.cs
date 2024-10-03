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
            var customerData = new Customer("John", "Doe");
            ICustomer customer = new CustomerService(customerData);

            var laptopData = new PhysicalProduct("Laptop", 1000.00m, 5, 2.5);
            var smartphoneData = new PhysicalProduct("Smartphone", 700.00m, 2, 0.3);
            var ebookData = new DigitalProduct("E-Book", 50.00m, 100, "http://example.com/download");

            var laptop = new ProductService(laptopData);
            var smartphone = new ProductService(smartphoneData);
            var ebook = new ProductService(ebookData);

            laptop.OutOfStockEvent += message => Console.WriteLine($"[EVENT] {message}");
            smartphone.OutOfStockEvent += message => Console.WriteLine($"[EVENT] {message}");
            ebook.OutOfStockEvent += message => Console.WriteLine($"[EVENT] {message}");

            laptop.DisplayDetails();
            smartphone.DisplayDetails();
            ebook.DisplayDetails();

            Console.WriteLine();

            var laptopOrder = new OrderService();
            var smartphoneOrder = new OrderService();
            var ebookOrder = new OrderService();
            var bulkOrder = new OrderService();

            if (laptopOrder.CreateOrder(customer, laptop, 3))
            {
                laptopOrder.ApplyDiscount(new PercentageDiscount(10));
                laptopOrder.SetPaymentMethod(new CreditCardPayment());
                laptopOrder.CompleteOrder();
            }

            Console.WriteLine();

            if (ebookOrder.CreateOrder(customer, ebook, 1))
            {
                ebookOrder.ApplyDiscount(new FixedDiscount(5));
                ebookOrder.SetPaymentMethod(new PayPalPayment());
                ebookOrder.CompleteOrder();
            }

            Console.WriteLine();

            if (smartphoneOrder.CreateOrder(customer, smartphone, 2))
            {
                smartphoneOrder.ApplyDiscount(new FixedDiscount(50));
                smartphoneOrder.SetPaymentMethod(new CreditCardPayment());
                smartphoneOrder.CompleteOrder();
            }

            Console.WriteLine();

            if (!laptopOrder.CreateOrder(customer, laptop, 3))
            {
                Console.WriteLine("Failed to create second laptop order due to insufficient stock.");
            }

            Console.WriteLine();

            if (bulkOrder.CreateOrder(customer, laptop, 2))
            {
                bulkOrder.ApplyDiscount(new BulkDiscount(2, 15));
                bulkOrder.SetPaymentMethod(new CreditCardPayment());
                bulkOrder.CompleteOrder();
            }

        }
    }
}