using OnlineStore.BLL.Interfaces;

namespace OnlineStore.BLL.Services
{
    public class BulkDiscount : IDiscount
    {
        private readonly int _threshold;
        private readonly decimal _discountPercentage;

        public BulkDiscount(int threshold, decimal discountPercentage)
        {
            _threshold = threshold;
            _discountPercentage = discountPercentage;
        }

        public decimal ApplyDiscount(decimal totalPrice, int quantity)
        {
            if (quantity > _threshold)
            {
                return totalPrice * (1 - _discountPercentage / 100);
            }
            return totalPrice;
        }
    }
}