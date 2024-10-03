using System;
using OnlineStore.BLL.Interfaces;

namespace OnlineStore.BLL.Services
{
    public class CreditCardPayment : IPayment
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing credit card payment of ${amount}");

            return true;
        }
    }

    public class PayPalPayment : IPayment
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing PayPal payment of ${amount}");

            return true;
        }
    }
}