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
            var customerData1 = new Customer("John", "Doe");
            ICustomer customer1 = new CustomerService(customerData1);

            var customerData2 = new Customer("Jane", "Smith");
            ICustomer customer2 = new CustomerService(customerData2);

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
            var multiProductOrder = new OrderService();

            if (laptopOrder.CreateOrder(customer1, laptop, 3))
            {
                laptopOrder.ApplyDiscount(new PercentageDiscount(10));
                laptopOrder.SetPaymentMethod(new CreditCardPayment());
                laptopOrder.CompleteOrder();
            }

            Console.WriteLine();

            if (ebookOrder.CreateOrder(customer1, ebook, 1))
            {
                ebookOrder.ApplyDiscount(new FixedDiscount(5));
                ebookOrder.SetPaymentMethod(new PayPalPayment());
                ebookOrder.CompleteOrder();
            }

            Console.WriteLine();

            if (smartphoneOrder.CreateOrder(customer1, smartphone, 2))
            {
                smartphoneOrder.ApplyDiscount(new FixedDiscount(50));
                smartphoneOrder.SetPaymentMethod(new CreditCardPayment());
                smartphoneOrder.CompleteOrder();
            }

            Console.WriteLine();

            if (!laptopOrder.CreateOrder(customer1, laptop, 3))
            {
                Console.WriteLine("Failed to create second laptop order due to insufficient stock.");
            }

            Console.WriteLine();

            if (bulkOrder.CreateOrder(customer1, laptop, 2))
            {
                bulkOrder.ApplyDiscount(new BulkDiscount(2, 15));
                bulkOrder.SetPaymentMethod(new CreditCardPayment());
                bulkOrder.CompleteOrder();
            }

            Console.WriteLine();

            multiProductOrder.SetCustomer(customer2);
            if (multiProductOrder.AddProduct(laptop, 1) && multiProductOrder.AddProduct(smartphone, 1) && multiProductOrder.AddProduct(ebook, 1))
            {
                multiProductOrder.ApplyDiscount(new PercentageDiscount(5));
                multiProductOrder.SetPaymentMethod(new CreditCardPayment());
                multiProductOrder.CompleteOrder();
            } // murzi me da slozha test bi trqbvalo da raboti multiple products order
        }
    }
}