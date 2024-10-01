using System;
using OnlineStore.BLL.Interfaces;
using OnlineStore.DAL.Models;

namespace OnlineStore.BLL.Services
{
    public class CustomerService : ICustomer
    {
        private readonly Customer _customer;

        public CustomerService(Customer customer)
        {
            _customer = customer;
        }

        public string FirstName => _customer.FirstName;
        public string LastName => _customer.LastName;

        public void DisplayInfo()
        {
            Console.WriteLine($"Customer: {FirstName} {LastName}");
        }
    }
}