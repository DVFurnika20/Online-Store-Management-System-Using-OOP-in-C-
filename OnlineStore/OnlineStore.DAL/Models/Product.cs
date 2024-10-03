namespace OnlineStore.DAL.Models
{
    public abstract class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        protected Product(string name, decimal price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }

        public abstract string GetProductType();
    }

    public class PhysicalProduct : Product
    {
        public double Weight { get; set; }

        public PhysicalProduct(string name, decimal price, int stock, double weight) 
            : base(name, price, stock)
        {
            Weight = weight;
        }

        public override string GetProductType() => "Physical";
    }

    public class DigitalProduct : Product
    {
        public string DownloadLink { get; set; }

        public DigitalProduct(string name, decimal price, int stock, string downloadLink) 
            : base(name, price, stock)
        {
            DownloadLink = downloadLink;
        }

        public override string GetProductType() => "Digital";
    }
}