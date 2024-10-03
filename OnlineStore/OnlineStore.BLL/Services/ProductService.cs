using System;
using OnlineStore.DAL.Models;

namespace OnlineStore.BLL.Services
{
    public class ProductService
    {
        private readonly Product _product;
        public event Action<string> OutOfStockEvent;

        public ProductService(Product product)
        {
            _product = product;
        }

        public string Name => _product.Name;
        public decimal Price => _product.Price;

        public bool DeductStock(int quantity)
        {
            if (_product.Stock >= quantity)
            {
                _product.Stock -= quantity;
                if (_product.Stock == 0)
                {
                    OutOfStockEvent?.Invoke($"{Name} is now out of stock!");
                }
                return true;
            }
            return false;
        }

        public int GetAvailableStock() => _product.Stock;

        public void DisplayDetails()
        {
            Console.WriteLine($"Product: {Name}, Type: {_product.GetProductType()}, Price: ${Price}, Stock: {_product.Stock}");
            if (_product is PhysicalProduct physical)
            {
                Console.WriteLine($"Weight: {physical.Weight} kg");
            }
            else if (_product is DigitalProduct digital)
            {
                Console.WriteLine($"Download Link: {digital.DownloadLink}");
            }
        }
    }
}